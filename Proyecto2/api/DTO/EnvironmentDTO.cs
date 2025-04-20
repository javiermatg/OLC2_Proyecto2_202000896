public class EnvironmentDTO {
    public string name { get; set; }
    public Dictionary<string, SymbolsDTO> variables { get; set; }
    

    //public List<SymbolsDTO> symbolList = new List<SymbolsDTO>();
    public List<TableSymbol> tableSymbols = new List<TableSymbol>();
    public EnvironmentDTO? parent { get; set; }
    public EnvironmentDTO? son { get; set; }
    public int lastPosition { get; set; }

    public EnvironmentDTO(string name, EnvironmentDTO? environment){
        this.name = name;
        this.variables = new Dictionary<string, SymbolsDTO>();
        this.parent = environment;
        this.lastPosition = 0;
        
        
    }

    public SymbolsDTO? seekVariable(string id){
        for (EnvironmentDTO? env = this; env != null; env = env.parent){
            if (env.variables.ContainsKey(id)){
                Console.WriteLine("Variable found in environment: " + env.name);
                return env.variables[id];
            }
        }

        for (EnvironmentDTO? env = this; env != null; env = env.son){
            if (env.variables.ContainsKey(id)){
                Console.WriteLine("Variable found in environment: " + env.name);
                return env.variables[id];
            }
        }
        return null;
    }

    public valueContentDTO addVariable(string id, SymbolsDTO Symbol){
        if (variables.ContainsKey(id)){
            Console.WriteLine("Variable already exists");
            throw new SemanticError("Variable already exists", Symbol.token);
        }
        else{
            variables.Add(id, Symbol);
            //variables[id] = Symbol;
            Console.WriteLine("Variable added");
            //symbolList.Add(Symbol);
            return variables[id].value;
        }
    }

    public valueContentDTO updateVariable(string id, valueContentDTO value){
        if (this.variables.ContainsKey(id)){
                SymbolsDTO symbol = this.variables[id];
                Console.WriteLine("Symbol.Type in update: " + symbol.type);
                Console.WriteLine("Value.Type: " + value.GetType().Name);
                if (symbol.type == "float64"){
                    symbol.type = symbol.type.Substring(0, symbol.type.Length - 2);
                }

                if((symbol.type+"Value").ToLower() != value.GetType().Name.ToLower()){
                    Console.WriteLine("Variable type mismatch");
                    throw new SemanticError("Variable type mismatch", symbol.token);
                }
                symbol.value = value;
                this.variables[id] = symbol;
                Console.WriteLine("Variable updated in environment: " + this.name);
                return value;
            }else {
                //Console.WriteLine("Variable not found in environment: " + this.name);
                for (EnvironmentDTO? env = this; env != null; env = env.parent){
            
                    if (env.variables.ContainsKey(id)){

                        
                        SymbolsDTO symbol = env.variables[id];
                        Console.WriteLine("Symbol.Type in update: " + symbol.type);
                        Console.WriteLine("Value.Type: " + value.GetType().Name);
                        if (symbol.type == "float64"){
                            symbol.type = symbol.type.Substring(0, symbol.type.Length - 2);
                        }

                        if((symbol.type+"Value").ToLower() != value.GetType().Name.ToLower()){
                            Console.WriteLine("Variable type mismatch");
                            throw new SemanticError("Variable type mismatch", symbol.token);
                        }
                        symbol.value = value;
                        env.variables[id] = symbol;
                        Console.WriteLine("Variable updated in environment: " + env.name);
                        return value;
                    }else {
                        Console.WriteLine("Variable not found in Update in environment: " + env.name);
                        //throw new SemanticError("Variable not found", null);
                    }
            }
            throw new SemanticError("Variable not found", null);
                
            }
            
        
    }

    public valueContentDTO autoIncrement(string id, valueContentDTO value, string sign){
        for (EnvironmentDTO? env = this; env != null; env = env.parent){
            if (env.variables.ContainsKey(id)){

                
                SymbolsDTO symbol = env.variables[id];
                Console.WriteLine("Symbol.Type in Autoincrement: " + symbol.type);
                Console.WriteLine("Value.Type: " + value.GetType().Name);
                if (symbol.type == "float64"){
                    symbol.type = symbol.type.Substring(0, symbol.type.Length - 2);
                }

                
                if((symbol.type+"Value").ToLower() != value.GetType().Name.ToLower()){
                    Console.WriteLine("Variable type mismatch");
                    throw new SemanticError("Variable type mismatch", symbol.token);
                }
                //symbol.value = value;
                var newValue = getValue(symbol.value, value, sign, symbol.token);
                symbol.value = newValue;

                
                
                env.variables[id] = symbol;
                Console.WriteLine("Variable autoincrement correct in environment: " + env.name);
                return newValue;
            }else {
                Console.WriteLine("Variable not found in environment: " + env.name);
                throw new SemanticError("Variable not found", null);
            }
            
            
        }
        throw new SemanticError("Variable not found", null);
    }

    public valueContentDTO getValue(valueContentDTO sValue,valueContentDTO value, string sign, Antlr4.Runtime.IToken token){
        return (sValue, value, sign) switch{
            (IntValue s, IntValue v, "+=") => new IntValue(s.Value + v.Value),
            (FloatValue s, FloatValue v, "+=") => new FloatValue(s.Value + v.Value),
            (IntValue s, IntValue v, "-=") => new IntValue(s.Value - v.Value),
            (FloatValue s, FloatValue v, "-=") => new FloatValue(s.Value - v.Value),
            (IntValue s, IntValue v, "++") => new IntValue(s.Value + 1),
            (FloatValue s, FloatValue v, "++") => new FloatValue(s.Value + 1),
            (IntValue s, IntValue v, "--") => new IntValue(s.Value - 1),
            (FloatValue s, FloatValue v, "--") => new FloatValue(s.Value - 1),

            _=> throw new SemanticError("Type mismatch AutoIncrement", token),

        };
        
    }

    public valueContentDTO incrementOne(string id, string sign){
        for (EnvironmentDTO? env = this; env != null; env = env.parent){
            if (env.variables.ContainsKey(id)){

                
                SymbolsDTO symbol = env.variables[id];

                var newValue = getValue(symbol.value, new IntValue(1), sign, symbol.token);
                symbol.value = newValue;
                env.variables[id] = symbol;
                Console.WriteLine("Variable incrementOne correct in environment: " + env.name);
                return newValue;
            }else{
                Console.WriteLine("Variable not found in environment: " + env.name);
            }
        }   
        throw new SemanticError("Variable not found", null);     
    }

    /*public string getName(){
        return this.name;
    }*/

    

    
}