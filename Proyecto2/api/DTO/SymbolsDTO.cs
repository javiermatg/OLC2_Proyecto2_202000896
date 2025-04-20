public class SymbolsDTO {
    public String id {get; set;}
    public string type {get; set;}
    public valueContentDTO? value {get; set;}

    public Antlr4.Runtime.IToken? token;
    public int line {get; set;}
    public int column {get; set;}

    //public FunctionDTO? function {get; set;}

    public SymbolsDTO(string id, string type, valueContentDTO value, Antlr4.Runtime.IToken? token){
        this.id = id;
        this.type = type;
        this.value = value;
        this.token = token;
        this.line = token.Line;
        this.column = token.Column;
    }
    /*
    public SymbolsDTO(string id, string type, FunctionDTO function, Antlr4.Runtime.IToken token){
        this.id = id;
        this.type = type;
        this.function = function;
        this.token = token;
        this.line = token.Line;
        this.column = token.Column;
    }*/

    public SymbolsDTO(string id, string type, Antlr4.Runtime.IToken token){
        this.id = id;
        this.type = type;
        this.token = token;
        this.line = token.Line;
        this.column = token.Column;
    }
}