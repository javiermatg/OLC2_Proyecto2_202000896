public class Function: Invocable{

    private EnvironmentDTO clousure;
    private lexicalAnalyzerParser.FuncStmtContext context;

    public Function(EnvironmentDTO clousure, lexicalAnalyzerParser.FuncStmtContext context){
        this.clousure = clousure;
        this.context = context;
    }

    public int Arity(){
        if(context.funcParams() == null){
            return 0;
        }
        return context.funcParams().ID().Length;
    }

    public valueContentDTO Invoke(List<valueContentDTO> arguments, InterpreterVisitor visitor){
    
        var newEnvironment = new EnvironmentDTO("function", clousure);
        visitor.stackEnvironment.Peek().son = newEnvironment;
        visitor.stackEnvironment.Push(newEnvironment);
        
        var beforeEnvironment = visitor.currentEnvironment;
        visitor.currentEnvironment = newEnvironment;

        if (context.funcParams() != null){
            for (int i = 0; i < context.funcParams().ID().Length; i++){
                newEnvironment.addVariable(context.funcParams().ID(i).GetText(), new SymbolsDTO(context.funcParams().ID(i).GetText(), "Function", arguments[i], context.Start));
            }
        }

        try{
            foreach (var instruction in context.lstinstructions().instruction()){
                visitor.VisitInstruction(instruction);

            }
        }catch(ReturnException e){
            visitor.currentEnvironment = beforeEnvironment;
            visitor.stackEnvironment.Pop();
            visitor.stackEnvironment.Peek().son = null;
            return e.Value;

        }

        visitor.currentEnvironment = beforeEnvironment;
        visitor.stackEnvironment.Pop();
        visitor.stackEnvironment.Peek().son = null;
        return visitor.defaultVoid;
    
    }


    public Function Bind(Instance instance){
        var envHidden = new EnvironmentDTO("metodo", clousure);
        envHidden.addVariable("this", new SymbolsDTO("this", "Instance", new InstanceValue(instance), context.Start));
        return new Function(envHidden, context);       
    }
}