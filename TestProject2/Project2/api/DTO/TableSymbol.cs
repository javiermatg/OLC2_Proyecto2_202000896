using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

public class TableSymbol {
    public string id { get; set; }
    public string typeSymbol { get; set; }
    public string typeData { get; set; }
    public string nameEnv { get; set; }
    public int line { get; set; }
    public int column { get; set; }

    public TableSymbol(string id, string typeSymbol, string typeData, string nameEnv, int line, int column){    
        this.id = id;
        this.typeSymbol = typeSymbol;
        this.typeData = typeData;   
        this.nameEnv = nameEnv;
        this.line = line;
        this.column = column;
    }
} 