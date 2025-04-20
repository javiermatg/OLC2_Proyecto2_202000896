using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("[controller]")]
    public class Compile : Controller
    {

        List<ErrorsDTO> errorsList = new List<ErrorsDTO>();
        List<ErrorsDTO> errorsLista = new List<ErrorsDTO>();
        List<TableSymbol> Tabla = new List<TableSymbol>();
        

        List<SymbolsDTO> symbolsList = new List<SymbolsDTO>();
        private readonly ILogger<Compile> _logger;

        public Compile(ILogger<Compile> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public class CompileRequest
        {
            [Required]
            public required string Code { get; set; }
        }

        // POST /compile
        [HttpPost]
        public IActionResult Post([FromBody] CompileRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Compiling code: {0}", request.Code);

            

            var inputStream = new AntlrInputStream(request.Code);
            var lexer = new lexicalAnalyzerLexer(inputStream);
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new CustomErrorListener(errorsList, "Error Léxico"));

            var tokenStream = new CommonTokenStream(lexer);
            var parser = new lexicalAnalyzerParser(tokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new CustomErrorListener(errorsList, "Error Sintáctico"));

            
            

            Console.WriteLine("========= This is List Errors");
            foreach (var error in errorsList)
            {
                
                Console.WriteLine(error);
                Console.WriteLine(error.errorType + " " + error.description + " " + error.line + " " + error.column);
            }
            try {
                var tree = parser.init();

                EnvironmentDTO initEnvironment = new EnvironmentDTO("main", null);
                var interpreter = new InterpreterVisitor(initEnvironment);
                interpreter.Visit(tree);

                var compiler = new CompilerVisitor();
                compiler.Visit(tree);
            
                
                
                //var result = visitor.Visit(tree);
                //Console.WriteLine(tree.ToStringTree());
                Console.WriteLine("------------Este es listout-------------");
                Console.WriteLine(interpreter.ListOut);
                foreach (var consola in interpreter.ListOut)
                    {
                        interpreter.output += consola.ToString() + "\n";

                    }
                
                
                int[] numeros = new int[10];
               
               
              
                
                Console.WriteLine("==== This is Stack");
                Console.WriteLine(interpreter.stackEnvironmentAux);
                var newType = "";
                foreach (var s in interpreter.stackEnvironmentAux)
                    {
                        
                        Console.WriteLine(s.name);
                        foreach (var par in s.variables){
                            Console.WriteLine(par.Key + " " + par.Value.id + " " + par.Value.type + " " + par.Value.value);
                            newType = par.Value.type;
                            if (par.Value.type == "float"){
                                newType = par.Value.type+"64";
                            }
                            TableSymbol newSymbol = new TableSymbol(par.Value.id, "Variable", newType, s.name, par.Value.token.Line, par.Value.token.Column);
                            Tabla.Add(newSymbol);
                            
                        }

                    }
                


                return Ok(new {result = compiler.g.ToString(),
                           errors = errorsList,
                           symbols = Tabla});    

            
            } catch (SemanticError e) {
                return BadRequest(new {result = e.Message});
            }
            
        }

        //Endpoint to Show the errors
        [HttpGet("errors")]
        public IActionResult GetErrors()
        {   
            Console.WriteLine("========= This is List Errors in get");
            Console.WriteLine(errorsLista);
            Console.WriteLine(errorsLista.Count);
            
            foreach (var error in errorsLista)
            {
                Console.WriteLine(error.errorType + " " + error.description + " " + error.line + " " + error.column);
            }

            return Ok(new {errors = errorsList});
        }


        public class CustomErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int> {
            public readonly List<ErrorsDTO> errorsList;
            public readonly string tipoError;

            public CustomErrorListener(List<ErrorsDTO> errorsList, string tipoError) {
                this.errorsList = errorsList;
                this.tipoError = tipoError;
            }

            public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
                errorsList.Add(new ErrorsDTO(tipoError, msg, line, charPositionInLine));
            }

            public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
                errorsList.Add(new ErrorsDTO(tipoError, msg, line, charPositionInLine));
            }
        }
    }
}