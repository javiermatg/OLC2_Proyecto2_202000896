public class LenguageClass : Invocable
{
    public string name {get; set;}
    public Dictionary<string, lexicalAnalyzerParser.StmtVarContext> Props {get; set;}
    public Dictionary<string, Function> Methods {get; set;}

    public LenguageClass(string name, Dictionary<string, lexicalAnalyzerParser.StmtVarContext> props, Dictionary<string, Function> methods){
        this.name = name;
        this.Props = props;
        this.Methods = methods;
    }

    public Function? getMethod(string id){
        if (Methods.ContainsKey(id)){
            return Methods[id];
        }
        return null;
    }

    public int Arity(){
        var constructor = getMethod("constructor");
        if (constructor != null){
            return constructor.Arity();
        }
        return 0;
    }

    public valueContentDTO Invoke(List<valueContentDTO> arguments, InterpreterVisitor visitor){
        var newInstance = new Instance(this);
        foreach(var prop in Props){
            var name = prop.Key;
            var value = prop.Value;

            if(value.expr() != null){
                var inValue = visitor.Visit(value.expr());  
                newInstance.Set(name, inValue);
            }else{
                newInstance.Set(name, visitor.defaultVoid);
            }
            
        }

        var constructor = getMethod("constructor");
        if (constructor != null){
            constructor.Bind(newInstance).Invoke(arguments, visitor);
        }

        return new InstanceValue(newInstance);
    }
    
   
}