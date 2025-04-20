
using System;
using System.Net;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;


public class InterpreterVisitor : lexicalAnalyzerBaseVisitor<valueContentDTO>
{   
    //public Dictionary<string, SymbolsDTO> Table = new Dictionary<string, SymbolsDTO>();
    //public List<TableSymbol> Table = new List<TableSymbol>();
    public valueContentDTO defaultVoid = new VoidValue();
    public List<Object> ListOut = new List<Object>();   
    //private Dictionary<string, Object> Variables = new Dictionary<string, Object>(); 
    public string output = "";

    public Stack<EnvironmentDTO> stackEnvironment = new Stack<EnvironmentDTO>();
    public Stack<EnvironmentDTO> stackEnvironmentAux = new Stack<EnvironmentDTO>();
    public EnvironmentDTO initialEnv; 

    public EnvironmentDTO currentEnvironment;
    
    public InterpreterVisitor(EnvironmentDTO env) {
        this.initialEnv = env;
        stackEnvironment.Push(env);
        stackEnvironmentAux.Push(env);
        
        
    }
    public override valueContentDTO VisitInit([NotNull] lexicalAnalyzerParser.InitContext context)
    {
        foreach (var instruction in context.instruction()){
            Visit(instruction);
        }
        return defaultVoid;
    }

   

    
    public override valueContentDTO VisitPrintVar([NotNull] lexicalAnalyzerParser.PrintVarContext context)
    {   /*
        if (context.expr() != null){
            valueContentDTO outPut = Visit(context.expr());
            ListOut.Add(outPut);
            Console.WriteLine("Aqui esta en print");
            Console.WriteLine(outPut);
            Console.WriteLine(ListOut);
        }*/
        
        Console.WriteLine("Here is in Print");
        if (context.expr() == null)
            return defaultVoid;

        foreach (lexicalAnalyzerParser.ExprContext expr in context.expr()){
            
            valueContentDTO value = Visit(expr);
            output += value switch
            {
                IntValue i => i.Value.ToString(),
                FloatValue f => f.Value.ToString(),
                StringValue s => s.Value.Trim('"'),
                BoolValue b => b.Value.ToString(),
                VoidValue v => "void",
                FunctionValue fn => "<fn " + fn.name + ">",
                _ => throw new SemanticError("Invalid value", context.Start)
            };
            //contador++;
        }
    
            output += "\n";
        
         // output += value + "\n";
        
        return defaultVoid;    
    }

    public override valueContentDTO VisitStmtVar([NotNull] lexicalAnalyzerParser.StmtVarContext context)
    {
        EnvironmentDTO currentEnv = stackEnvironment.Peek();
        string type = context.type().GetText();
        string id = context.ID().GetText();        
        //valueContentDTO? value = null;
        
                 
        Console.WriteLine("Here is in stmtVar");
        if(context.expr() != null){
            
            valueContentDTO value = Visit(context.expr());
            Console.WriteLine(value.GetType().Name);
            var typeValue = type+"Value" ;
            if(type == "float64"){
                typeValue = "floatValue";
            }
            
            Console.WriteLine(typeValue);
            if (value.GetType().Name.ToLower() != typeValue.ToLower()){
                throw new SemanticError("Invalid type Stmt", context.Start);
            }
            currentEnv.addVariable(id, new SymbolsDTO(id, type, value, context.Start));
            Console.WriteLine(value);

        }else {
            valueContentDTO value = DefaultValue(type, context.Start);
            currentEnv.addVariable(id, new SymbolsDTO(id, type, value, context.Start));
            Console.WriteLine(value);
        }
                //value = DefaultValue(type);
            
            
            
            //Console.WriteLine("Here is in stmtVar");
            //Console.WriteLine(currentEnv.name);
            //Table.Add(new TableSymbol(id, "Variable", type, currentEnv.name, context.Start.Line, context.Start.Column));
            //Console.WriteLine(Table);
        
        return defaultVoid; //new BreakDTO("continue");     
        

    }

    public valueContentDTO DefaultValue(string type, Antlr4.Runtime.IToken token)
    {
        if (type == "int")
            return new IntValue(0);
        else if (type == "float64")
            return new FloatValue(0.0f);
        else if (type == "string")
            return new StringValue("\"\"");
        else if (type == "rune")
            return new StringValue("");
        else if (type == "bool")
            return new BoolValue(false);
        else
            throw new SemanticError("Invalid type Default", token);
        /*
        return type switch{
            "int" => 0,
            "float" => 0.0,
            "string" => "",
            "rune" => 0,
            "bool" => false,
            _ => throw new Exception("Invalid type")
        };*/
    }
    // Modification StmtVarAssign
    public override valueContentDTO VisitStmtVarAssign([NotNull] lexicalAnalyzerParser.StmtVarAssignContext context)
    {   
        EnvironmentDTO currentEnv = stackEnvironment.Peek();
        string id = context.ID().GetText();
        valueContentDTO value = Visit(context.expr());
        var type = getValueType(value);
        return currentEnv.addVariable(id, new SymbolsDTO(id, type, value, context.Start));
            
        
    }
    public override valueContentDTO VisitAssingVar([NotNull] lexicalAnalyzerParser.AssingVarContext context)
    {
        EnvironmentDTO currentEnv = stackEnvironment.Peek();
        string id = context.ID().GetText();
        string sign = context.GetChild(1).GetText();
        if (context.expr() != null){
            valueContentDTO value = Visit(context.expr());
            /*
            if (sign == ":=")
            {
            Console.WriteLine("Entro a := ");
            
            if (context.expr() != null)
                value = Visit(context.expr());*/
            /*else 
                valor = ValorPorDefecto(valor.GetType());*/
            /*    
            var type = getValueType(value);
            return currentEnv.addVariable(id, new SymbolsDTO(id, type, value, context.Start));
            
            //Table.Add(new TableSymbol(id,"Variable", "int", currentEnv.name, context.Start.Line, context.Start.Column));
                
            
            } else*/ if (sign == "="){
            //validar que el valor sea del mismo tipo
            /*if(!currentEnv.variables.ContainsKey(id))
                throw new SemanticError("Variable not found", context.Start);*/
            Console.WriteLine("Heres is in =");
            return currentEnv.updateVariable(id, value);   
            
            } else {
            return currentEnv.autoIncrement(id, value, sign);
            }

        }else {
            return currentEnv.incrementOne(id, sign);
        }


        
        

        
        //return defaultVoid;
    }
    public string getValueType(valueContentDTO value)
    {
        return value switch
        {
            IntValue i => "int",
            FloatValue f => "float",
            StringValue s => "string",
            BoolValue b => "bool",
            _ => throw new SemanticError("Invalid value", null)
        };
    }

    public override valueContentDTO VisitIfStmt([NotNull] lexicalAnalyzerParser.IfStmtContext context)
    {   
        Console.WriteLine("Here is in If");
        valueContentDTO exprIf = Visit(context.exprIf);
        //valueContentDTO exprelseIf = Visit(context.exprElseIf);
        //dynamic fromIf = Visit(context.lstinstructions(0));
        
        if (exprIf is not BoolValue){
            throw new SemanticError("Expected boolean expression", context.Start);
        }
        if ((exprIf as BoolValue).Value){
            EnvironmentDTO currentEnv = new EnvironmentDTO("If", stackEnvironment.Peek());
                
            stackEnvironment.Peek().son = currentEnv;
            stackEnvironment.Push(currentEnv);
            stackEnvironmentAux.Push(currentEnv);
            Visit(context.lsIf);
            stackEnvironment.Pop();
            stackEnvironment.Peek().son = null;
        }else {
            if(context.exprElseIf != null){
                valueContentDTO exprelseIf = Visit(context.exprElseIf);
                if (exprelseIf is BoolValue && (exprelseIf as BoolValue).Value){
                //Console.WriteLine("Emtra a Else if Bool");
                    EnvironmentDTO currentEnv = new EnvironmentDTO("ElseIf", stackEnvironment.Peek());
                    stackEnvironment.Peek().son = currentEnv;
                    stackEnvironment.Push(currentEnv);
                    stackEnvironmentAux.Push(currentEnv);
                    Visit(context.lsElseIf);
                    stackEnvironment.Pop();
                    stackEnvironment.Peek().son = null;
                }else {
                    if (context.lsElse != null){
                        EnvironmentDTO currentEnv = new EnvironmentDTO("Else", stackEnvironment.Peek());
                        stackEnvironment.Peek().son = currentEnv;
                        stackEnvironment.Push(currentEnv);
                        stackEnvironmentAux.Push(currentEnv);
                        Visit(context.lsElse);
                        stackEnvironment.Pop();
                        stackEnvironment.Peek().son = null;
                    }
                }
            }else {
                    if (context.lsElse != null){
                        EnvironmentDTO currentEnv = new EnvironmentDTO("Else", stackEnvironment.Peek());
                        stackEnvironment.Peek().son = currentEnv;
                        stackEnvironment.Push(currentEnv);
                        stackEnvironmentAux.Push(currentEnv);
                        Visit(context.lsElse);
                        stackEnvironment.Pop();
                        stackEnvironment.Peek().son = null;
                    }
                    }
        }
        
        return defaultVoid;
    }

    /* public override valueContentDTO VisitFuncStmt([NotNull] lexicalAnalyzerParser.FuncStmtContext context)
     {
         Dictionary<string, SymbolsDTO> parameters = new Dictionary<string, SymbolsDTO>();
         List<String> paramsType = new ArrayList<string>();

         if (context.funcParams() != null){un
             var funcParams = context.funcParams();
             var expressions = funcParams.ID();
             var types = funcParams.type();
             for (int i = 0; i < expressions.Length; i++){
                 //valueContentDTO value = Visit(expressions[i]);
                 string type = types[i].GetText();
                 string idParam = expressions[i].GetText();
                 parameters.Add(idParam, new SymbolsDTO(idParam, type, context.Start));
                 paramsType.Add(type);
             }

         }
         Console.WriteLine("Here is in FuncStmt");
         Console.WriteLine(context.returnT.GetText());   
         string returnType = "";
         if (context.returnT != null)
             returnType = context.returnT.GetText();
         else 
             returnType = "voidddd";        
         //string returnType = context.returnT != null ? context.returnT.GetText() : "void";
         Console.WriteLine("Return type: ",  returnType);
         SymbolsDTO returnSymbol = new SymbolsDTO("return"+context.ID().GetText(), returnType, context.Start);
         parameters.Add("return"+context.ID().GetText(), returnSymbol);

         FunctionDTO function = new FunctionDTO(context.ID().GetText(), parameters, context.lstinstructions(), paramsType, returnSymbol);
         stackEnvironment.Peek().addVariable(context.ID().GetText(), new SymbolsDTO(context.ID().GetText(), "function", function, context.Start));
         Console.WriteLine("Function added");
         return defaultVoid;
     }

     public override valueContentDTO VisitFuncExecute([NotNull] lexicalAnalyzerParser.FuncExecuteContext context)
     {
         Console.WriteLine("Here is in execute function");
         EnvironmentDTO currentEnv = stackEnvironment.Peek();
         SymbolsDTO? symbol = currentEnv.seekVariable(context.ID().GetText());
         if (symbol != null && symbol.function  is FunctionDTO function){
             EnvironmentDTO envFunc = new EnvironmentDTO("Function", currentEnv);
             stackEnvironment.Push(envFunc);
             //Visit(function.instructions);

             var retu = Visit(function.instructions);
             Console.WriteLine("Here is in INstructions function");
             Console.WriteLine(retu);
             stackEnvironment.Pop();
             stackEnvironment.Peek().son = null;

             if (function.returnValue != null){
                 Console.WriteLine("There is return value");
                 Console.WriteLine(function.returnValue);
                 Console.WriteLine(function.returnValue.id);
                 Console.WriteLine(function.returnValue.type);
                 if (function.returnValue.value != null){
                     Console.WriteLine("Inside return value .value");
                     return retu;
                 }

             }
             return retu;
         }else
             throw new SemanticError("Function does not exist in current environment", context.Start);      

     }*/
    public override valueContentDTO VisitFuncStmt([NotNull] lexicalAnalyzerParser.FuncStmtContext context)
    {
        Console.WriteLine("Here is in FuncStmt");
        EnvironmentDTO currentEnv = stackEnvironment.Peek();

        var function = new Function(currentEnv, context );
        currentEnv.addVariable(context.ID().GetText(), new SymbolsDTO(context.ID().GetText(), "function", new FunctionValue(function, context.ID().GetText()), context.Start));
        return defaultVoid;
    }
    public override valueContentDTO VisitFunctionCall([NotNull] lexicalAnalyzerParser.FunctionCallContext context)
    {
        Console.WriteLine("Here is in FunctionCall");
        valueContentDTO valueF = Visit(context.expr());

        foreach(var f in context.func()){
            if (valueF is FunctionValue functionValue){
            Console.WriteLine("Here is in FunctionCall If");
            valueF = VisitFunc(functionValue.invocable, f.pars());
            }else{

                throw new SemanticError("Expected function", context.Start);
            }
        }
            

        return valueF;
    }

    public valueContentDTO VisitFunc(Invocable invocable, lexicalAnalyzerParser.ParsContext context)
    {
        List<valueContentDTO> args = new List<valueContentDTO>();
        if (context != null)
        {
            foreach (var expr in context.expr())
            {
                args.Add(Visit(expr));
            }
        }
        return invocable.Invoke(args, this);

    }

    

    

    public override valueContentDTO VisitConvertInt([NotNull] lexicalAnalyzerParser.ConvertIntContext context)
    {
        valueContentDTO value = Visit(context.expr());
        Console.WriteLine(value);
        if (value is not StringValue)
            throw new SemanticError("Expected string value to convert INT", context.Start);

        Console.WriteLine("Here is in ConvertInt");
        if (int.TryParse((value as StringValue).Value, out int result))
            return new IntValue(result);
        else
            throw new SemanticError("Invalid conversion to int", context.Start);
        //return new IntValue(int.Parse((value as StringValue).Value));
    }

    public override valueContentDTO VisitConvertFloat([NotNull] lexicalAnalyzerParser.ConvertFloatContext context)
    {
        valueContentDTO value = Visit(context.expr());
        if (value is not StringValue)
            throw new SemanticError("Expected string value to convert FLOAT", context.Start);

        if (float.TryParse((value as StringValue).Value, out float result))
            return new FloatValue(result);
        else
            throw new SemanticError("Invalid conversion to float", context.Start);
    }

    public override valueContentDTO VisitTypeOf([NotNull] lexicalAnalyzerParser.TypeOfContext context)
    {
        valueContentDTO value = Visit(context.expr());
        return value switch
        {
            IntValue i => new StringValue("int"),
            FloatValue f => new StringValue("float64"),
            StringValue s => new StringValue("string"),
            BoolValue b => new StringValue("bool"),
            _ => throw new SemanticError("Invalid value", context.Start)
        };

    }
    public override valueContentDTO VisitForStmt([NotNull] lexicalAnalyzerParser.ForStmtContext context)
    {
        
        if (context.forDeclare(0) != null){
            EnvironmentDTO previusEnv = currentEnvironment;
            //currentEnvironment= new EnvironmentDTO("For", currentEnvironment); 
            currentEnvironment= new EnvironmentDTO("For", stackEnvironment.Peek()); 
            stackEnvironment.Peek().son = currentEnvironment;
            stackEnvironment.Push(currentEnvironment);
            stackEnvironmentAux.Push(currentEnvironment);
            Visit(context.forDeclare(0));
            VisitBody(context);

            //currentEnv = beforeEnv;
            stackEnvironment.Pop();
            stackEnvironment.Peek().son = null;

            currentEnvironment = previusEnv;
        }else {
            Console.WriteLine("Here is in For-While");
            var sonEnvironment = currentEnvironment;
            EnvironmentDTO currentEnv = new EnvironmentDTO("For", stackEnvironment.Peek());
                
            stackEnvironment.Peek().son = currentEnv;
            stackEnvironment.Push(currentEnv);
            VisitBodyWhile(context);
            
            stackEnvironment.Pop();
            stackEnvironment.Peek().son = null;      

        }
        
        //EnvironmentDTO beforeEnv = currentEnv;
        
        //stackEnvironment.Push(envFunc);


        
        return defaultVoid;
    
    }

    public void VisitBodyWhile(lexicalAnalyzerParser.ForStmtContext context)
    {
        valueContentDTO conditionFor = Visit(context.expr());
        var sonEnvironment = currentEnvironment;
            if (conditionFor is not BoolValue)
                throw new SemanticError("Expected boolean expression in For-While", context.Start);   
            
            try{
                while((conditionFor as BoolValue).Value){
                    Visit(context.lsfor);          

                    conditionFor = Visit(context.expr());
                }
            }
            catch (BreakException)
            {
                stackEnvironment.Pop();
                stackEnvironment.Peek().son = null;
                return ;
            }catch (ContinueException)
            {
                //stackEnvironment.Pop();
                //stackEnvironment.Peek().son = null;
                currentEnvironment = sonEnvironment;
                Visit(context.expr());
                VisitBodyWhile(context);
                //VisitBody(context);
            }
    }    

    public void VisitBody(lexicalAnalyzerParser.ForStmtContext context)
    {
        valueContentDTO condition = Visit(context.expr());
        //EnvironmentDTO currentEnv = stackEnvironment.Peek();
        var sonEnvironment = currentEnvironment;
        if (condition is not BoolValue)
            throw new SemanticError("Expected boolean expression in For", context.Start);


        try
        {
            while (condition is BoolValue boolCondition && boolCondition.Value)
            {

                Console.WriteLine("Entry in While");
                Visit(context.lsfor);
                
                Visit(context.forDeclare(1));
                condition = Visit(context.expr());
            }
        }
        catch (BreakException)
        {
            currentEnvironment = sonEnvironment;
            
            return;
        }
        catch (ContinueException)
        {
            currentEnvironment = sonEnvironment;
            Visit(context.forDeclare(1));
            VisitBody(context);
            
            
        }
       
    }

    public override valueContentDTO VisitSwitchStmt([NotNull] lexicalAnalyzerParser.SwitchStmtContext context)
    {   
        Console.WriteLine("Here is in Switch");
        if (context.expr() == null)
            throw new SemanticError("Invalid switch expression", context.Start);

        EnvironmentDTO currentEnv = new EnvironmentDTO("Switch", stackEnvironment.Peek());
        stackEnvironment.Peek().son = currentEnv;
        stackEnvironment.Push(currentEnv);
        stackEnvironmentAux.Push(currentEnv);    

        valueContentDTO value = Visit(context.expr());
        var cases = context.cases();
        var expressions = cases.expr();
        var instructions = cases.instruction();

        try{
            for (int i = 0; i < expressions.Length; i++)
            {
                valueContentDTO caseValue = Visit(expressions[i]);
                if (value.Equals(caseValue))
                {
                    Visit(instructions[i]);
                    stackEnvironment.Pop();
                    stackEnvironment.Peek().son = null;
                    //break;
                    return defaultVoid;
                }

            }
        }
        catch (BreakException)
        {
            stackEnvironment.Pop();
            stackEnvironment.Peek().son = null;
            return defaultVoid;
        }    
        Visit(context.lsDefautl);
        Console.WriteLine("Here is Out Switch");
        return defaultVoid;
    }

    public override valueContentDTO VisitReturnStmt([NotNull] lexicalAnalyzerParser.ReturnStmtContext context)
    {
        Console.WriteLine("Here is in Return");
        valueContentDTO value = defaultVoid;
        if (context.expr() != null)
            value = Visit(context.expr());
            Console.WriteLine(value);
        throw new ReturnException(value);
    }

    /*public override valueContentDTO VisitFunctionExecute([NotNull] lexicalAnalyzerParser.FunctionExecuteContext context)
    {
        return VisitFuncExecute(context.funcExecute());
    }*/

    public override valueContentDTO VisitBreakTransfer([NotNull] lexicalAnalyzerParser.BreakTransferContext context)
    {
        throw new BreakException();
    }

    public override valueContentDTO VisitContinueTransfer([NotNull] lexicalAnalyzerParser.ContinueTransferContext context)
    {
        throw new ContinueException();
    }

    
    public override valueContentDTO VisitAddSub([NotNull]lexicalAnalyzerParser.AddSubContext context)
    {   

        /*dynamic left = (int) Visit(context.expr(0));
        int right = (int) Visit(context.expr(1));

        return context.GetChild(1).GetText() == "+" ? left + right : left - right;*/
        Console.WriteLine("Here is in AddSub");
        Console.WriteLine("op: " + context.GetChild(1).GetText());
        if (context.expr(0) == null || context.expr(1) == null)
            throw new SemanticError("Invalid operation nil Relational", context.Start);
        valueContentDTO left = Visit(context.GetChild(0));
        valueContentDTO right = Visit(context.expr(1));
        Console.WriteLine("Left: "+ left + "right: " + right);
        var op = context.GetChild(1).GetText();

        return (left, right, op) switch{
            (IntValue l, IntValue r, "+") => new IntValue(l.Value + r.Value),
            (IntValue l, IntValue r, "-") => new IntValue(l.Value - r.Value),
            (IntValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (IntValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (FloatValue l, IntValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, IntValue r, "-") => new FloatValue(l.Value - r.Value),
            (FloatValue l, FloatValue r, "+") => new FloatValue(l.Value + r.Value),
            (FloatValue l, FloatValue r, "-") => new FloatValue(l.Value - r.Value),
            (StringValue l, StringValue r, "+") => new StringValue(l.Value + r.Value),
            (IntValue l, StringValue r, "+") => new StringValue(l.Value.ToString() + r.Value), 
            (StringValue l, IntValue r, "+") => new StringValue(l.Value + r.Value.ToString()),  
            _ => throw new SemanticError("Invalid operation add", context.Start)
        };
    
    }

    public override valueContentDTO VisitMulDiv([NotNull]lexicalAnalyzerParser.MulDivContext context)
    {
        /*
        int left = (int) Visit(context.expr(0));
        int right = (int) Visit(context.expr(1));

        return context.GetChild(1).GetText() == "*" ? left * right : left / right;*/

        valueContentDTO left = Visit(context.expr(0));
        valueContentDTO right = Visit(context.expr(1));
        var op = context.GetChild(1).GetText();

        return (left, right, op) switch
        {
            (IntValue l, IntValue r, "*") => new IntValue(l.Value * r.Value),
            (IntValue l, IntValue r, "/") => new IntValue(l.Value / r.Value),
            (FloatValue l, FloatValue r, "*") => new FloatValue(l.Value * r.Value),
            (FloatValue l, FloatValue r, "/") => new FloatValue(l.Value / r.Value),
            _ => throw new SemanticError("Invalid operation multDiv", context.Start)
        };

    }

    public override valueContentDTO VisitModule([NotNull] lexicalAnalyzerParser.ModuleContext context)
    {
        valueContentDTO left = Visit(context.expr(0));
        valueContentDTO right = Visit(context.expr(1));
        if (left is not IntValue || right is not IntValue)
            throw new SemanticError("Expected integer values", context.Start);
        return new IntValue((left as IntValue).Value % (right as IntValue).Value);
    }

    public override valueContentDTO VisitLogicOperator([NotNull] lexicalAnalyzerParser.LogicOperatorContext context)
    {
        //var (boolVall, opl) = ((BoolValue, string))Visit(context.left)!;
        valueContentDTO left = Visit(context.left);
        valueContentDTO right = Visit(context.right);
        var op = context.GetChild(1).GetText();
        Console.WriteLine("Here is in Logic");
        Console.WriteLine(left);
        return (left, right, op) switch
        {
            (BoolValue l, BoolValue r, "&&") => new BoolValue(l.Value && r.Value),
            (BoolValue l, BoolValue r, "||") => new BoolValue(l.Value || r.Value),
            _ => throw new SemanticError("Invalid operationLogic", context.Start)
        };
        
        /*
        return operators switch
        {
            "&&" => left == right,
            "||" => left != right,
            _ => throw new Exception("Invalid operator")
        };*/            
    }

    public override valueContentDTO VisitNegateOperator([NotNull] lexicalAnalyzerParser.NegateOperatorContext context)
    {   

        valueContentDTO expr = Visit(context.expr());
        return expr switch {
            BoolValue b => new BoolValue(!b.Value),
            _ => throw new SemanticError("Invalid operationNegateO", context.Start)
        };
        
        /*
        if((bool) Visit(context.right))
            return false;
        else 
            return true;*/
    }

    public override valueContentDTO VisitRelationalOperator([NotNull] lexicalAnalyzerParser.RelationalOperatorContext context)
    {   
        if (context.left == null || context.right == null)
            throw new SemanticError("Invalid operation nil Relational", context.Start);
        valueContentDTO left = Visit(context.left);
        valueContentDTO right = Visit(context.right);   
        
        var op = context.GetChild(1).GetText();
        Console.WriteLine("Here is in Relational");
        Console.WriteLine(left);
        return (left,right, op) switch {
            (IntValue l, IntValue r, "==") => new BoolValue(l.Value == r.Value),
            (IntValue l, IntValue r, "!=") => new BoolValue(l.Value != r.Value),
            (FloatValue l, FloatValue r, "==") => new BoolValue(l.Value == r.Value),
            (FloatValue l, FloatValue r, "!=") => new BoolValue(l.Value != r.Value),
            (StringValue l, StringValue r, "==") => new BoolValue(l.Value == r.Value),
            (StringValue l, StringValue r, "!=") => new BoolValue(l.Value != r.Value),
            (BoolValue l, BoolValue r, "==") => new BoolValue(l.Value == r.Value),
            (BoolValue l, BoolValue r, "!=") => new BoolValue(l.Value != r.Value),
            (IntValue l, IntValue r, "<") => new BoolValue(l.Value < r.Value),
            (IntValue l, IntValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (IntValue l, IntValue r, ">") => new BoolValue(l.Value > r.Value),
            (IntValue l, IntValue r, ">=") => new BoolValue(l.Value >= r.Value),
            (FloatValue l, FloatValue r, "<") => new BoolValue(l.Value < r.Value),
            (FloatValue l, FloatValue r, "<=") => new BoolValue(l.Value <= r.Value),
            (FloatValue l, FloatValue r, ">") => new BoolValue(l.Value > r.Value),
            (FloatValue l, FloatValue r, ">=") => new BoolValue(l.Value >= r.Value),
            _ => throw new SemanticError("Invalid operation Relational", context.Start)
        };
        /*
        string operators = context.GetChild(1).GetText();
        dynamic left = Visit(context.left);
        dynamic right = Visit(context.right);   

        return operators switch
        {
            "<" => left < right,
            ">" => left > right,
            "<=" => left <= right,
            ">=" => left >= right,
            "==" => left == right,
            "!=" => left != right,
            _ => throw new Exception("Invalid operator")
        };*/
    }
    public override valueContentDTO VisitNegate([NotNull] lexicalAnalyzerParser.NegateContext context)
    {   
        Console.WriteLine("Here is in negate (-)");
        valueContentDTO expr = Visit(context.expr());
        return expr switch {
            IntValue i => new IntValue(-i.Value),
            FloatValue f => new FloatValue(-f.Value),
            _ => throw new SemanticError("Invalid operation negate", context.Start)
        };
    }
    public override valueContentDTO VisitNumber([NotNull]lexicalAnalyzerParser.NumberContext context)
    {   
        return new IntValue(int.Parse(context.INT().GetText()));
    }

    public override valueContentDTO VisitCharacter([NotNull] lexicalAnalyzerParser.CharacterContext context)
    {
        return new StringValue(context.CHARACTER().GetText());
    }

    public override valueContentDTO VisitDecimal([NotNull] lexicalAnalyzerParser.DecimalContext context)
    {
        return new FloatValue(float.Parse(context.DECIMAL().GetText()));
    }


    public override valueContentDTO VisitString([NotNull] lexicalAnalyzerParser.StringContext context)
    {   
        Console.WriteLine("Here is in string");
        //Console.WriteLine("Here is in string");
        //Console.WriteLine(context.STRING().GetText());
        //Console.WriteLine(context.GetText());
        Console.WriteLine(context.STRING().GetText().Trim('"'));
        return new StringValue(context.STRING().GetText().Trim('"'));
    }

    public override valueContentDTO VisitIdentifier([NotNull] lexicalAnalyzerParser.IdentifierContext context)
    {
        Console.WriteLine("Here is in identifier");
        EnvironmentDTO currentEnv = stackEnvironment.Peek();
        //string id = context.ID().GetText();
        SymbolsDTO? symbol = currentEnv.seekVariable(context.ID().GetText());
        //Console.WriteLine("Aqui está en identifier");
        //Console.WriteLine(symbol.value.GetType());
        //Console.WriteLine(symbol.value);
        if (symbol != null){
            return symbol.value;
        }else
            //SemanticError idnot = new SemanticError("Variable not found", context.Start);
            throw new SemanticError("Variable not found identifier", context.Start);
    }
    public override valueContentDTO VisitBoolean([NotNull] lexicalAnalyzerParser.BooleanContext context)
    {
        return new BoolValue(bool.Parse(context.BOOL().GetText()));

    }    

    public override valueContentDTO VisitNull([NotNull] lexicalAnalyzerParser.NullContext context)
    {
        return null;
    }

    public override valueContentDTO VisitParens(lexicalAnalyzerParser.ParensContext context)
    {
        return Visit(context.expr());
    }

    public override valueContentDTO VisitStmtExpr([NotNull] lexicalAnalyzerParser.StmtExprContext context)
    {
        return Visit(context.expr());
    }



}
