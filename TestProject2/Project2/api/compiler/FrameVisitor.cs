using Antlr4.Runtime.Misc;

public class FrameElement{
    public string Name {get; set;}
    public int Offset {get; set;}

    public FrameElement(string name, int offset){
        Name = name;
        Offset = offset;
    }
}

public class FrameVisitor : lexicalAnalyzerBaseVisitor<Object?>{
    public List<FrameElement> Frame;
    public int LocalOffset;
    public int BaseOffset;
    public FrameVisitor(int baseOffset){
        Frame = new List<FrameElement>();
        LocalOffset = 0;
        BaseOffset = baseOffset;
    }

   
    public override object VisitStmtVar([NotNull] lexicalAnalyzerParser.StmtVarContext context)
    {
        string name = context.ID().GetText();
        Frame.Add(new FrameElement(name, BaseOffset+LocalOffset));
        LocalOffset += 1;
        return null;
    }
    public override object VisitIfStmt([NotNull] lexicalAnalyzerParser.IfStmtContext context)
    {
        Visit(context.lstinstructions(0));
        if(context.expr().Length > 1){
            Visit(context.lstinstructions(1));
        }
        return null;
    }

    public override object VisitForStmt([NotNull] lexicalAnalyzerParser.ForStmtContext context)
    {
        if (context.stmtAssign() != null){
            Visit(context.stmtAssign());
            Visit(context.lstinstructions());
        }else{

            Visit(context.lstinstructions());
        }
        return null;
    }
    /*public override object? VisitBlockStmt(lexicalAnalyzerParser.BlockStmtContext context){
        foreach (var dcl in context.dcl()){
            Visit(dcl)
        }
        return null;
    }*/
    
}
