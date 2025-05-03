
using System;
using System.Net;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.SignalR;

public class FunctionMetadata{
    public int FrameSize;
    public StackObject.StackObjectType ReturnType;
}
public class CompilerVisitor : lexicalAnalyzerBaseVisitor<Object?>
{   
    //public Dictionary<string, SymbolsDTO> Table = new Dictionary<string, SymbolsDTO>();
    //public List<TableSymbol> Table = new List<TableSymbol>();
    //public Object? defaultVoid = new VoidValue();
    public ArmGenerator g = new ArmGenerator();
    public int contadorIf = 0;
    //private String? continueLabel = null;
    private string continueLabel = "";
    //private String? breakLabel = null;
    private string breakLabel = "";
    //private String? returnLabel = null;
    private string returnLabel = "";
    private Dictionary<string, FunctionMetadata> functions = new Dictionary<string, FunctionMetadata>();
    private string? insideFunction = null;
    private int framePointerOffset = 0;
    public CompilerVisitor() {
        
        
    }
    public override Object? VisitInit([NotNull] lexicalAnalyzerParser.InitContext context){
        if(context.lstinstructions() != null)
            return VisitLstinstructions(context.lstinstructions());
        return null;
    }
    public override Object? VisitLstinstructions([NotNull] lexicalAnalyzerParser.LstinstructionsContext context){
        if (context.instruction() == null)
            return null;
        
        foreach (lexicalAnalyzerParser.InstructionContext instruction in context.instruction())
        {
            VisitInstruction(instruction);
        }
        return null;
    }
    public override Object? VisitInstruction([NotNull] lexicalAnalyzerParser.InstructionContext context){
        if (context == null)
            return null;

        if (context.print() !=null)
            Visit(context.print());
        else if (context.stmtVariables() != null)
            Visit(context.stmtVariables());
        //else if(context.assign() != null)
         //   Visit(context.assign());
        else if (context.instructionIf() != null)
            Visit(context.instructionIf());
        else if (context.funcInstructions() != null)
            Visit(context.funcInstructions());
        //else if (context.funExecute() != null)
        //    Visit(context.funExecute());
        else if(context.returnFunc() != null)
            Visit(context.returnFunc());   
        else if(context.forInstruction() != null)      
            Visit(context.forInstruction());
        else if(context.breakInstruction() != null)    
            Visit(context.breakInstruction());
        else if(context.continueInstruction() != null)
            Visit(context.continueInstruction());
        else if(context.switchInstruction() != null)
            Visit(context.switchInstruction());    
        else if(context.expression() != null)    
            Visit(context.expression());
        else if(context.stmtAssign() != null)    
            Visit(context.stmtAssign());
        return null;
    }
    public override Object? VisitPrintVar([NotNull] lexicalAnalyzerParser.PrintVarContext context){
        g.Comment("Print Expr");
        g.Comment("Visiting Expr");
        if(context.expr().Length == 1){
            Visit(context.expr(0));
            var isFloat = g.PeekObject().Type == StackObject.StackObjectType.Float;
            var value = g.PopObject(isFloat ? Register.D0 : Register.X0); //pop the value to print
            if (value.Type == StackObject.StackObjectType.Int){
                g.PrintInt(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Float){
                g.PrintFloat();
            } else if (value.Type == StackObject.StackObjectType.String){
                g.PrintStringSalto(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Bool){
                g.PrintInt(Register.X0);
            }
            //g.PrintSalto();
            return null;                    
        }
        foreach(lexicalAnalyzerParser.ExprContext expr in context.expr()){
            Visit(expr);
            g.Comment("Popping value to print");
            var isFloat = g.PeekObject().Type == StackObject.StackObjectType.Float;
            var value = g.PopObject(isFloat ? Register.D0 : Register.X0); //pop the value to print
            if (value.Type == StackObject.StackObjectType.Int){
                g.PrintInt(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Float){
                g.PrintFloat();
            } else if (value.Type == StackObject.StackObjectType.String){
                g.PrintString(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Bool){
                g.PrintInt(Register.X0);
            } /*else if (value.Type == StackObject.StackObjectType.Null){
                g.PrintNull(Register.X0);
            }*/

        }
        g.PrintSalto();
        //Visit(context.expr());
        return null;
    }
    public override Object? VisitStmtVar([NotNull] lexicalAnalyzerParser.StmtVarContext context){
        string type = context.type().GetText();
        var name = context.ID().GetText();
        
        if (context.expr() != null){
            g.Comment("Visiting StmtVar: " + name);
            Visit(context.expr());
        }else{     
            if (type == "int"){
                g.Comment("Number: " + 0);
                var intObject = g.IntObject();
                g.PushConstant(intObject, int.Parse("0"));
            } else if (type == "float64"){
                g.Comment("Decimal Value: " + 0.0);
                var floatObject = g.FloatObject();
                g.PushConstant(floatObject, double.Parse("0.0"));
            } else if (type == "string"){
                g.Comment("String Value: " + "");
                var stringObject = g.StringObject();
                g.PushConstant(stringObject, "");
            } else if (type == "bool"){
                g.Comment("Visiting Boolean");
                g.Comment("Boolean Value: " + false);
                var boolObject = g.BoolObject();
                g.PushConstant(boolObject, false );
            }
        }

        if(insideFunction != null){
            var localObject = g.GetFrameLocal(framePointerOffset);
            var valueObject = g.PopObject(Register.X0);
            
            g.Mov(Register.X1, framePointerOffset*8);// maybe doesnt work   localObject.Offset en framePointerOffset
            g.Sub(Register.X1, Register.FP, Register.X1);
            g.Str(Register.X0, Register.X1);
            localObject.Type = valueObject.Type;
            framePointerOffset++;
            return null;
        }    
        g.tagObject(name);
        return null;
    }
    public override Object? VisitStmtVarAssign([NotNull] lexicalAnalyzerParser.StmtVarAssignContext context){
        var id = context.ID().GetText();
        g.Comment("Visiting AssignVar := " + id);
        Visit(context.expr());
        g.tagObject(id);
        return null;
    }
    public override Object? VisitAssingVar([NotNull] lexicalAnalyzerParser.AssingVarContext context){
        var id = context.ID().GetText();
        var sign = context.GetChild(1).GetText();
        g.Comment("Visiting AssingVar: " + id);

        
            
           
            
        if(insideFunction != null){
            Visit(context.expr());
            var valueObject = g.PopObject(Register.X0);
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X1, varObject.Offset *8);
            g.Sub(Register.X1, Register.FP, Register.X1);
            g.Str(Register.X0, Register.X1);
            return null;
        }
            
        
        //Para id -> expr
        /*if (id is lexicalAnalyzerParser.IdentifierContext idContext){
            g.Comment("Visiting ID: " + id);
            Visit(idContext);
        }*/
        
        /*if (sign == ":="){
            g.Comment("Visiting AssingVar :=: " + id);
            Visit(context.expr());
            g.tagObject(id);
        } else*/ if(sign == "="){
            Visit(context.expr());
            var value = g.PopObject(Register.X0);
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X1, offset);
            g.Add(Register.X1, Register.SP, Register.X1);
            g.Str(Register.X0, Register.X1); 
            //varObject.Type = value.Type;
            g.Push(Register.X0); //pubsh the value back to the stack
            g.PushObject(g.CloneObject(varObject)); //push the variable object to the stack
        } else if(sign == "+="){
            g.Comment("Visiting AssingVar +=: " + id);
            
            g.Comment("looking for id");
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X0, offset);
            g.Add(Register.X0, Register.SP, Register.X0); // add the offset to sp to get the address
            g.Ldr(Register.X0, Register.X0); // load de value from the address
           
            g.Push(Register.X0);
            var copyObject = g.CloneObject(varObject);
            copyObject.Id = null;
            g.PushObject(copyObject); // push the  copy object to the stack
            g.Comment("Visiting Expr to +=");
            Visit(context.expr());
            var value = g.PopObject(Register.X1);
            var valueB = g.PopObject(Register.X0);
            g.Comment("Adding values");
            g.Add(Register.X0, Register.X0, Register.X1); // add the value to the variable
            g.Comment("Pushing Result");
            g.Push(Register.X0);
            g.PushObject(g.CloneObject(value)); 
            //=====
            var newvalue = g.PopObject(Register.X0);
            var (newoffset, newvarObject) = g.getObject(id);
            g.Mov(Register.X1, newoffset);
            g.Add(Register.X1, Register.SP, Register.X1);
            g.Str(Register.X0, Register.X1); 
            //varObject.Type = value.Type;
            g.Push(Register.X0); //pubsh the value back to the stack
            g.PushObject(g.CloneObject(newvarObject));
        }else if(sign == "-="){
            g.Comment("Visiting AssingVar -=: " + id);
            
            g.Comment("looking for id");
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X0, offset);
            g.Add(Register.X0, Register.SP, Register.X0); // add the offset to sp to get the address
            g.Ldr(Register.X0, Register.X0); // load de value from the address
           
            g.Push(Register.X0);
            var copyObject = g.CloneObject(varObject);
            copyObject.Id = null;
            g.PushObject(copyObject); // push the  copy object to the stack
            g.Comment("Visiting Expr to +=");
            Visit(context.expr());
            var value = g.PopObject(Register.X1);
            var valueB = g.PopObject(Register.X0);
            g.Comment("subtracting values");
            g.Sub(Register.X0, Register.X0, Register.X1); // sub the value to the variable
            g.Comment("Pushing Result");
            g.Push(Register.X0);
            g.PushObject(g.CloneObject(value)); 
            //=====
            var newvalue = g.PopObject(Register.X0);
            var (newoffset, newvarObject) = g.getObject(id);
            g.Mov(Register.X1, newoffset);
            g.Add(Register.X1, Register.SP, Register.X1);
            g.Str(Register.X0, Register.X1); 
            //varObject.Type = value.Type;
            g.Push(Register.X0); //pubsh the value back to the stack
            g.PushObject(g.CloneObject(newvarObject));
        }else if(sign == "++"){
            g.Comment("Visiting AssingVar ++: " + id);
            
            g.Comment("looking for id");
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X0, offset);
            g.Add(Register.X0, Register.SP, Register.X0); // add the offset to sp to get the address
            g.Ldr(Register.X0, Register.X0); // load de value from the address
           
            g.Push(Register.X0);
            var copyObject = g.CloneObject(varObject);
            copyObject.Id = null;
            g.PushObject(copyObject); // push the  copy object to the stack
            
            
            var value = g.PopObject(Register.X0);
            g.Comment("adding values");
            g.Add(Register.X0, Register.X0, "#1"); // sub the value to the variable
            g.Comment("Pushing Result");
            g.Push(Register.X0);
            g.PushObject(g.CloneObject(value)); 
            //=====
            var newvalue = g.PopObject(Register.X0);
            var (newoffset, newvarObject) = g.getObject(id);
            g.Mov(Register.X1, newoffset);
            g.Add(Register.X1, Register.SP, Register.X1);
            g.Str(Register.X0, Register.X1); 
            //varObject.Type = value.Type;
            g.Push(Register.X0); //pubsh the value back to the stack
            g.PushObject(g.CloneObject(newvarObject));
        }else if(sign == "--"){
            g.Comment("Visiting AssingVar ++: " + id);
            
            g.Comment("looking for id");
            var (offset, varObject) = g.getObject(id);
            g.Mov(Register.X0, offset);
            g.Add(Register.X0, Register.SP, Register.X0); // add the offset to sp to get the address
            g.Ldr(Register.X0, Register.X0); // load de value from the address
           
            g.Push(Register.X0);
            var copyObject = g.CloneObject(varObject);
            copyObject.Id = null;
            g.PushObject(copyObject); // push the  copy object to the stack
            
            
            var value = g.PopObject(Register.X0);
            g.Comment("adding values");
            g.Sub(Register.X0, Register.X0, "#1"); // sub the value to the variable
            g.Comment("Pushing Result");
            g.Push(Register.X0);
            g.PushObject(g.CloneObject(value)); 
            //=====
            var newvalue = g.PopObject(Register.X0);
            var (newoffset, newvarObject) = g.getObject(id);
            g.Mov(Register.X1, newoffset);
            g.Add(Register.X1, Register.SP, Register.X1);
            g.Str(Register.X0, Register.X1); 
            //varObject.Type = value.Type;
            g.Push(Register.X0); //pubsh the value back to the stack
            g.PushObject(g.CloneObject(newvarObject));
        }
            
        
        return null;
    }
    public override Object? VisitIfStmt([NotNull] lexicalAnalyzerParser.IfStmtContext context){
        g.Comment("Visiting IfStmt");
        g.newScope();
        
        Visit(context.exprIf);
        g.PopObject(Register.X0);

        var thereElse = context.lstinstructions().Length > 1;
        if (thereElse){
            var elseLabel = g.GetLabel();
            var endLabel = g.GetLabel();
            g.Cbz(Register.X0, elseLabel);
            Visit(context.lstinstructions(0));
            g.B(endLabel);
            g.SetLabel(elseLabel);
            Visit(context.lstinstructions(1));
            g.SetLabel(endLabel);
        
        }else {
            var endLabel = g.GetLabel();
            g.Cbz(Register.X0, endLabel);
            Visit(context.lstinstructions(0));
            g.SetLabel(endLabel);
        }
        /*
        g.newScope();
        foreach (var instruction in context.instruction()){
            Visit(instruction);
        }
        int bytesToRemove = g.endScope();
        if (bytesToRemove > 0){
            g.Comment("Removing " + bytesToRemove + " bytes from stack");
            g.Mov(Register.X0, bytesToRemove); 
            g.Add(Register.SP, Register.SP, Register.X0);
            g.Comment("Stack pointer adjusted to " + Register.SP);
        }
        g.Label($"endif{contadorIf}");
        contadorIf++;*/
        int bytesToRemove = g.endScope();
        if (bytesToRemove > 0){
            g.Comment("Removing " + bytesToRemove + " bytes from stack");
            g.Mov(Register.X0, bytesToRemove); 
            g.Add(Register.SP, Register.SP, Register.X0);
            g.Comment("Stack pointer adjusted to " + Register.SP);
        }
        return null;
    }
    public override Object? VisitFuncStmt([NotNull] lexicalAnalyzerParser.FuncStmtContext context){
        int baseOffset = 2;
        int parameterOffset = 0;
        if(context.@funcParams() != null){
            
            parameterOffset = context.@funcParams().ID().Length;
        }
        FrameVisitor frameVisitor = new FrameVisitor(baseOffset + parameterOffset);
        foreach(var instruction in context.lstinstructions().instruction()){
            frameVisitor.VisitInstruction(instruction);
        }

        var frame = frameVisitor.Frame;
        int localOffset = frame.Count;
        int returnOffset = 1;
        int totalFrameSize = baseOffset + parameterOffset + localOffset + returnOffset;
        string funcName = context.ID().GetText();
        
        //StackObject.StackObjectType funcType = (context.type().GetText() ? "int" : "float64");
        if (context.returnT != null){
            StackObject.StackObjectType funcType = GetObjectType(context.returnT.GetText() ) ;
            Console.WriteLine("Total Frame: "+ totalFrameSize);
            functions.Add(funcName, new FunctionMetadata{
                FrameSize = totalFrameSize,
                ReturnType = funcType
            });
        }
        

        
        
        var beforeInstructions = g.instructions;
        g.instructions = new List<string>();
        var paramCounter = 0;
        /*foreach(var param in context.@funcParams().type()){
            g.PushObject(new StackObject{
                Type = GetObjectType(param.GetText()),
                Id = param,
                Offset = baseOffset + paramCounter,
                Length = 8
            });
        }*/
        for(int i = 0; i < context.@funcParams().ID().Length; i++){
            //var id = context.@funcParams().ID(i).GetText();
            g.PushObject(new StackObject{
                Type = GetObjectType(context.@funcParams().type(i).GetText()),
                Id = context.@funcParams().ID(i).GetText(),
                Offset = paramCounter +baseOffset,
                Length = 8
            });
            paramCounter++;
        }
        foreach(FrameElement element in frame){
            g.PushObject(new StackObject{
                Type = StackObject.StackObjectType.Undefined,
                Id = element.Name,
                Offset = element.Offset,
                Length = 8
            });
        }
        insideFunction = funcName;
        framePointerOffset = 0;
        returnLabel = g.GetLabel();
        g.Comment("Visiting FuncStmt");
        g.Comment("Visiting Func: " + funcName);
        g.SetLabel(funcName);
        foreach(var instruction in context.lstinstructions().instruction()){
            VisitInstruction(instruction);
        }
        g.SetLabel(returnLabel);
        g.Add(Register.X0, Register.FP, Register.XZR);
        g.Ldr(Register.LR, Register.X0);
        g.Br(Register.LR);
        g.Comment("End Func: " + funcName);
        for(int i = 0;i<parameterOffset+localOffset;i++){
            g.PopObj();
        }
        foreach(var instruction in g.instructions){
            g.instructionsFunc.Add(instruction);
        }
        g.instructions = beforeInstructions;
        insideFunction = null;


        return null;
    }
    public override Object? VisitFunctionCall([NotNull] lexicalAnalyzerParser.FunctionCallContext context){
        if (context.expr() is not lexicalAnalyzerParser.IdentifierContext idContext) return null;
        string funcName = idContext.ID().GetText();
        var call = context.func()[0];
        //if (call is not lexicalAnalyzerParser.ParsContext parsContext) return null;
        //if (call is not lexicalAnalyzerParser.FuncCallContext parsContext) return null;
        var postFuncCallLable = g.GetLabel();
        int baseOffset = 2;
        int stackElementSize = 8;
        g.Mov(Register.X0, baseOffset * stackElementSize);
        g.Sub(Register.SP, Register.SP, Register.X0);
        if (context.func() != null){
            g.Comment("Visiting Function Params!");
            foreach(var param in context.func()){
                Visit(param);
                
            }
        }

        g.Mov(Register.X0, stackElementSize * (baseOffset + context.func().Length));
        g.Add(Register.SP, Register.SP, Register.X0);

        g.Mov(Register.X0, stackElementSize);
        g.Sub(Register.X0, Register.SP, Register.X0);

        g.Adr(Register.X1, postFuncCallLable);
        g.Push(Register.X1);

        g.Push(Register.FP);
        g.Add(Register.FP, Register.X0, Register.XZR);
        
        int frameSize = functions[funcName].FrameSize;
        g.Mov(Register.X0, (frameSize - 2) * stackElementSize);
        g.Sub(Register.SP, Register.SP, Register.X0);

        g.Comment("Visiting Function Call: " + funcName);
        g.Bl(funcName);
        g.Comment("Function Call finished");
        g.SetLabel(postFuncCallLable);

        var returnOffset = frameSize - 1;
        g.Mov(Register.X4, returnOffset * stackElementSize);
        g.Sub(Register.X4, Register.FP, Register.X4);
        g.Ldr(Register.X4, Register.X4);
        
        g.Mov(Register.X1, stackElementSize);
        g.Sub(Register.X1, Register.FP, Register.X1);
        g.Ldr(Register.FP, Register.X1);
        
        g.Mov(Register.X0, stackElementSize * frameSize);
        g.Add(Register.SP, Register.SP, Register.X0);

        g.Push(Register.X4);
        g.PushObject(new StackObject{
            Type = functions[funcName].ReturnType,
            Id = null,
            Offset = 0,
            Length = 8
        });

        g.Comment("End of Function Call: " + funcName);

        return null;
    }
    public override Object? VisitReturnStmt([NotNull] lexicalAnalyzerParser.ReturnStmtContext context){
        g.Comment("Visiting ReturnStmt");
        if(context.expr() == null){
            g.Br(returnLabel);
            return null;
        }
        //if (insideFunction == null) throw new Exception("Return statement outside function");

        Visit(context.expr());
        g.PopObject(Register.X0);
        var frameSize = functions[insideFunction].FrameSize;
        var returnOffset = frameSize - 1;
        g.Mov(Register.X1, returnOffset * 8);
        g.Sub(Register.X1, Register.FP, Register.X1);
        g.Str(Register.X0, Register.X1);
        g.B(returnLabel);
        g.Comment("End ReturnStmt");

        return null;
    }
    public override Object? VisitConvertInt([NotNull] lexicalAnalyzerParser.ConvertIntContext context){
        return null;
    }
    public override Object? VisitConvertFloat([NotNull] lexicalAnalyzerParser.ConvertFloatContext context){
        return null;
    }
    public override Object? VisitTypeOf([NotNull] lexicalAnalyzerParser.TypeOfContext context){
        return null;
    }
    public override Object? VisitForStmt([NotNull] lexicalAnalyzerParser.ForStmtContext context){
        if (context.stmtAssign() != null){
            //Visit(context.forDeclare(0));
            g.Comment("Visiting  Clasic For");
            var startLable = g.GetLabel();
            var endlabel = g.GetLabel();
            var incrementLabel = g.GetLabel();

            // transference values
            var beforeContinueLabel = continueLabel;
            var beforeBreakLabel = breakLabel;

            continueLabel = incrementLabel;
            breakLabel = endlabel;
            g.Comment("Visiting ForDeclare");
            g.newScope();
            Visit(context.stmtAssign());
            g.SetLabel(startLable);
            g.Comment("Visiting Expr");
            Visit(context.expr());
            g.PopObject(Register.X0);
            g.Cbz(Register.X0, endlabel);
            g.Comment("Visiting Instruction");
            Visit(context.lstinstructions());
            g.SetLabel(incrementLabel);
            Visit(context.forDeclare());
            g.B(startLable);
            g.SetLabel(endlabel);
            g.Comment("End For");
            var bytesToRemove = g.endScope();
            if (bytesToRemove > 0){
                g.Comment("Removing " + bytesToRemove + " bytes from stack");
                g.Mov(Register.X0, bytesToRemove); 
                g.Add(Register.SP, Register.SP, Register.X0);
                g.Comment("Stack pointer adjusted to " + Register.SP);
            }
            // restore labels
            continueLabel = beforeContinueLabel;
            breakLabel = beforeBreakLabel;

        } else{
            g.Comment("Visiting ForWhile");
            var startLable = g.GetLabel();
            var endLabel = g.GetLabel();
            // transference values
            var beforeContinueLabel = continueLabel;
            var beforeBreakLabel = breakLabel;  
            continueLabel = startLable;
            breakLabel = endLabel;

            g.SetLabel(startLable);
            Visit(context.expr());
            g.PopObject(Register.X0);
            g.Cbz(Register.X0, endLabel);
            Visit(context.lstinstructions());
            g.B(startLable);
            g.SetLabel(endLabel);
            g.Comment("End ForWhile");

            // restore labels
            
            continueLabel = beforeContinueLabel;
            breakLabel = beforeBreakLabel;
        }
        return null;
    }
    public override Object? VisitSwitchStmt([NotNull] lexicalAnalyzerParser.SwitchStmtContext context){
        //g.Comment("Visiting IfStmt");        
        
        //Visit(context.expr());
        //g.PopObject(Register.X0);
        /*var thereElse = context.lstinstructions().Length > 1;
        if (thereElse){
            var elseLabel = g.GetLabel();
            var endLabel = g.GetLabel();
            g.Cbz(Register.X0, elseLabel);
            Visit(context.lstinstructions(0));
            g.B(endLabel);
            g.SetLabel(elseLabel);
            Visit(context.lstinstructions(1));
            g.SetLabel(endLabel);
        
        }else {
            var endLabel = g.GetLabel();
            g.Cbz(Register.X0, endLabel);
            Visit(context.lstinstructions(0));
            g.SetLabel(endLabel);
        }*/
        /*var cases = context.cases();
        var expressions = cases.expr();
        var instructions = cases.lstinstructions();

        
        for (int i = 0; i < expressions.Length; i++)
        {   
            if (i == 0){
                Visit(expressions[i]);
                g.PopObject(Register.X1);
                g.Cmp(Register.X1, Register.X0);
                var falseLabelI = g.GetLabel();
                var endLabelI = g.GetLabel();
                g.Bne(falseLabelI);                        
                Visit(instructions[i]);

                g.B(endLabelI);
                g.SetLabel(falseLabelI);
                g.SetLabel(endLabel);
            } else {
                g.Comment("Visiting Expr");
                g.Mov(Register.X0, Register.X1);
            }
            Visit(expressions[i]);
            g.PopObject(Register.X1);
            g.Cmp(Register.X1, Register.X0);
            var falseLabel = g.GetLabel();
            var endLabel = g.GetLabel();
            g.Bne(falseLabel);                        
            Visit(instructions[i]);

            g.B(endLabel);
            g.SetLabel(falseLabel);
            g.SetLabel(endLabel);
            if (i+1 == expressions.Length){
                g.SetLabel(endLabel);
            }
            //Visit(context.lstinstructions(1));
            //g.SetLabel(endLabel);
            /*valueContentDTO caseValue = Visit(expressions[i]);
            if (value.Equals(caseValue))
            {
                Visit(instructions[i]);
                stackEnvironment.Pop();
                stackEnvironment.Peek().son = null;
                //break;
                return defaultVoid;
            }*/
        //}
        
        
        
        
        return null;
    }
    
    public override Object? VisitBreakTransfer([NotNull] lexicalAnalyzerParser.BreakTransferContext context){
        g.Comment("Visiting BreakTransfer");
        if (breakLabel != null){
            g.B(breakLabel);
        }
        return null;
    }
    public override Object? VisitContinueTransfer([NotNull] lexicalAnalyzerParser.ContinueTransferContext context){
        g.Comment("Visiting ContinueTransfer");
        if (continueLabel != null){
            g.B(continueLabel);
        }
        //g.B(continueLabel);
        return null;
    }
    StackObject.StackObjectType GetObjectType(string name){
        switch (name)
        {
            case "int":
                return StackObject.StackObjectType.Int;
            case "float64":
                return StackObject.StackObjectType.Float;
            case "string":
                return StackObject.StackObjectType.String;
            case "bool":
                return StackObject.StackObjectType.Bool;
            default:
                throw new ArgumentException("Invalid Function return Type");
        }
    }
    public override Object? VisitAddSub([NotNull]lexicalAnalyzerParser.AddSubContext context){
        g.Comment("Visiting AddSub");
        var op = context.GetChild(1).GetText();

        g.Comment("Visiting Expr left");
        Visit(context.expr(0));
        g.Comment("Visiting Expr right");
        Visit(context.expr(1));

        g.Comment("Popping values");
        var rightDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var right = g.PopObject(rightDouble ? Register.D0 : Register.X0); // Ya se tiene el tipo de valor a operar
        var leftDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var left = g.PopObject(leftDouble ? Register.D1 : Register.X1);
        g.Comment("Comparing values float");
        if (leftDouble || rightDouble){
            if (!leftDouble) g.Scvtf(Register.D1, Register.X1); //convert left to double
            if (!rightDouble) g.Scvtf(Register.D0, Register.X0); //convert left to double

            if (op == "+"){
                g.Fadd(Register.D0, Register.D0, Register.D1);
            } else if (op == "-"){
                g.Fsub(Register.D0, Register.D1, Register.D0);
            }
            g.Comment("Pushing Result");
            g.Push(Register.D0);
            g.PushObject(g.CloneObject(leftDouble ? left : right)); // se clona el valor con el tipo que tiene más predominancia
            return null;
        }

        if (op == "+"){
            g.Add(Register.X0, Register.X0, Register.X1);
        } else if (op == "-"){
            g.Sub(Register.X0, Register.X1, Register.X0);
        }
        g.Comment("Pushing Result");
        g.Push(Register.X0);
        g.PushObject(g.CloneObject(left)); // se clona el valor con el tipo que tiene más predominancia
        return null;
    }
    public override Object? VisitMulDiv([NotNull]lexicalAnalyzerParser.MulDivContext context){
        g.Comment("Visiting MulDiv");
        var op = context.GetChild(1).GetText();

        g.Comment("Visiting Expr left");
        Visit(context.expr(0));
        g.Comment("Visiting Expr right");
        Visit(context.expr(1));

        g.Comment("Popping values");
        var rightDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var right = g.PopObject(rightDouble ? Register.D0 : Register.X0); // Ya se tiene el tipo de valor a operar
        var leftDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var left = g.PopObject(leftDouble ? Register.D1 : Register.X1);
        g.Comment("Comparing values float");
        if (leftDouble || rightDouble){
            if (!leftDouble) g.Scvtf(Register.D1, Register.X1); //convert left to double
            if (!rightDouble) g.Scvtf(Register.D0, Register.X0); //convert left to double

            if (op == "*"){
                g.Fmul(Register.D0, Register.D0, Register.D1);
            } else if (op == "/"){
                g.Fdiv(Register.D0, Register.D1, Register.D0);
            }
            
            g.Comment("Pushing Result");
            g.Push(Register.D0);
            g.PushObject(g.CloneObject(leftDouble ? left : right)); // se clona el valor con el tipo que tiene más predominancia
            return null;
        }

        if (op == "*"){
            g.Mul(Register.X0, Register.X0, Register.X1);
        } else if (op == "/"){
            g.SDiv(Register.X0, Register.X1, Register.X0);
        }
        g.Comment("Pushing Result");
        g.Push(Register.X0);
        g.PushObject(g.CloneObject(left)); 
        return null;
    }
    public override Object? VisitModule([NotNull] lexicalAnalyzerParser.ModuleContext context){
        g.Comment("Visiting MulDiv");
        var op = context.GetChild(1).GetText();

        g.Comment("Visiting Expr left");
        Visit(context.expr(0));
        g.Comment("Visiting Expr right");
        Visit(context.expr(1));

        g.Comment("Popping values");
        var rightDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var right = g.PopObject(rightDouble ? Register.D0 : Register.X0); // Ya se tiene el tipo de valor a operar
        var leftDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var left = g.PopObject(leftDouble ? Register.D1 : Register.X1);
        g.Comment("Comparing values float");
        if (leftDouble || rightDouble){
            if (!leftDouble) g.Scvtf(Register.D1, Register.X1); //convert left to double
            if (!rightDouble) g.Scvtf(Register.D0, Register.X0); //convert left to double

            g.SDiv(Register.X2, Register.X1, Register.X0);
            g.Msub(Register.X0, Register.X2, Register.X1, Register.X0);
            
            g.Comment("Pushing Result");
            g.Push(Register.D0);
            g.PushObject(g.CloneObject(leftDouble ? left : right)); // se clona el valor con el tipo que tiene más predominancia
            return null;
        }

        g.SDiv(Register.X2, Register.X1, Register.X0);
        g.Msub(Register.X0, Register.X2, Register.X0, Register.X1);
        g.Comment("Pushing Result");
        g.Push(Register.X0);
        g.PushObject(g.CloneObject(left)); 
        return null;
    }
    public override Object? VisitLogicOperator([NotNull] lexicalAnalyzerParser.LogicOperatorContext context){
        var left = Visit(context.left);
        var right = Visit(context.right);
        var op = context.GetChild(1).GetText();
        Console.WriteLine("Here is in Logic");
        Console.WriteLine(left);
        Console.WriteLine(right);
        return null;
        
    }
    public override Object? VisitNegateOperator([NotNull] lexicalAnalyzerParser.NegateOperatorContext context){
        /*var value = Visit(context.expr());
        var boolObject = g.BoolObject();
        g.PushConstant(boolObject, value is true ? false : true );*/
        var value = Visit(context.expr());
        var boolObject = g.BoolObject();
        g.Eor(Register.X0, Register.X0);
        g.Push(Register.X0);
        g.PushObject(g.BoolObject()); // se clona el valor con el tipo que tiene más predominancia
        return null;
    }
    public override Object? VisitRelationalOperator([NotNull] lexicalAnalyzerParser.RelationalOperatorContext context){
        var op = context.GetChild(1).GetText();
        g.Comment("Visiting RelationalOperator");
        g.Comment("Visiting Expr left");
        Visit(context.expr(0));
        g.Comment("Visiting Expr right");
        Visit(context.expr(1));
        g.Comment("Popping values");
        var rightDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var right = g.PopObject(rightDouble ? Register.D0 : Register.X0); // Ya se tiene el tipo de valor a operar
        var leftDouble = g.PeekObject().Type == StackObject.StackObjectType.Float;
        var left = g.PopObject(leftDouble ? Register.D1 : Register.X1);

        if (leftDouble || rightDouble){
            if (!leftDouble) g.Scvtf(Register.D1, Register.X1); //convert left to double
            if (!rightDouble) g.Scvtf(Register.D0, Register.X0); //convert left to double

            g.Comment("Comparing values");
            g.Fcmp(Register.D1, Register.D0);
            var trueLabelD = g.GetLabel();
            var endLabelD = g.GetLabel();

            switch(op){
                case "==":
                    g.Beq(trueLabelD);
                    break;
                case "!=":
                    g.Bne(trueLabelD);
                    break;
                case "<":
                    g.Blt(trueLabelD);
                    break;
                case "<=":
                    g.Ble(trueLabelD);
                    break;
                case ">":
                    g.Bgt(trueLabelD);
                    break;
                case ">=":
                    g.Bge(trueLabelD);
                    break;
            }
            g.Mov(Register.X0, 0);
            g.Push(Register.X0);
            g.B(endLabelD);
            g.SetLabel(trueLabelD);
            g.Mov(Register.X0, 1);
            g.Push(Register.X0);
            g.SetLabel(endLabelD);
            g.PushObject(g.BoolObject()); 
            return null;
        }

        
        g.Comment("Comparing values");
        g.Cmp(Register.X1, Register.X0);
        var trueLabel = g.GetLabel();
        var endLabel = g.GetLabel();

        switch(op){
            case "==":
                g.Beq(trueLabel);
                break;
            case "!=":
                g.Bne(trueLabel);
                break;
            case "<":
                g.Blt(trueLabel);
                break;
            case "<=":
                g.Ble(trueLabel);
                break;
            case ">":
                g.Bgt(trueLabel);
                break;
            case ">=":
                g.Bge(trueLabel);
                break;
        }
        g.Mov(Register.X0, 0);
        g.Push(Register.X0);
        g.B(endLabel);
        g.SetLabel(trueLabel);
        g.Mov(Register.X0, 1);
        g.Push(Register.X0);
        g.SetLabel(endLabel);
        g.PushObject(g.BoolObject()); // se clona el valor con el tipo que tiene más predominancia
        /*g.Cmp(Register.X1, Register.X0);
        if (op == "=="){
            g.Bne($"endif{contadorIf}");
            return new BoolValue(true);
        }else if(op == "!="){
            g.Beq($"endif{contadorIf}");
            return new BoolValue(true);
        }else if(op == "<"){
            g.Bge($"endif{contadorIf}");
            return new BoolValue(true);
        }else if(op == "<="){
            g.Bgt($"endif{contadorIf}");
            return new BoolValue(true);
        }else if(op == ">"){
            g.Ble($"endif{contadorIf}");
            return new BoolValue(true);
        }else if(op == ">="){
            g.Blt($"endif{contadorIf}");
            return new BoolValue(true);
        }*/
        //return new BoolValue(true);
        return null;
        
    }
    public override Object? VisitNegate([NotNull] lexicalAnalyzerParser.NegateContext context){
        return null;
    }
    public override Object? VisitNumber([NotNull]lexicalAnalyzerParser.NumberContext context){
        var value = context.INT().GetText();
        g.Comment("Number: " + value);

        var intObject = g.IntObject();
        g.PushConstant(intObject, int.Parse(value));

        //g.Mov(Register.X0, int.Parse(value));
        //g.Push(Register.X0);
        return null;
    }
    public override Object? VisitCharacter([NotNull] lexicalAnalyzerParser.CharacterContext context){
        return null;
    }
    public override Object? VisitDecimal([NotNull] lexicalAnalyzerParser.DecimalContext context){
        var value = context.DECIMAL().GetText();
        g.Comment("Decimal Value: " + value);
        var floatObject = g.FloatObject();
        g.PushConstant(floatObject, double.Parse(value));

        return null;
    }
    public override Object? VisitString([NotNull] lexicalAnalyzerParser.StringContext context){
        var value = context.STRING().GetText().Trim('"');
        g.Comment("String Value: " + value);
        var stringObject = g.StringObject();
        g.PushConstant(stringObject, value);
        return null;
    }
    public override Object? VisitIdentifier([NotNull] lexicalAnalyzerParser.IdentifierContext context){
        var id = context.ID().GetText();
        g.Comment("Visiting ID: " + id);
        var (offset, varObject) = g.getObject(id);

        if(insideFunction != null){
            g.Mov(Register.X0, varObject.Offset * 8);
            g.Sub(Register.X0, Register.FP, Register.X0);
            g.Ldr(Register.X0, Register.X0); // load de value from the address
            g.Push(Register.X0);
            var CloneObject = g.CloneObject(varObject);
            CloneObject.Id = null;
            g.PushObject(CloneObject); // push the  copy object to the stack
            return null;
        }

        g.Mov(Register.X0, offset);
        g.Add(Register.X0, Register.SP, Register.X0); // add the offset to sp to get the address        
        g.Ldr(Register.X0, Register.X0); // load de value from the address
        g.Push(Register.X0);
        var copyObject = g.CloneObject(varObject);
        copyObject.Id = null;
        g.PushObject(copyObject); // push the  copy object to the stack
        return null;
    }
    public override Object? VisitBoolean([NotNull] lexicalAnalyzerParser.BooleanContext context){
        var value = context.BOOL().GetText();
        g.Comment("Visiting Boolean");
        g.Comment("Boolean Value: " + value);
        var boolObject = g.BoolObject();
        g.PushConstant(boolObject, value == "true" ? true : false );
        return null;
    }
    public override Object? VisitNull([NotNull] lexicalAnalyzerParser.NullContext context){
        return null;
    }
    public override Object? VisitParens(lexicalAnalyzerParser.ParensContext context){
        return null;
    }
    public override Object? VisitStmtExpr([NotNull] lexicalAnalyzerParser.StmtExprContext context){
        g.Comment("Visiting StmtExpr");
        Visit(context.expr());
        g.Comment("Popping value to discard in expr");
        g.PopObject(Register.X0);
        return null;
    }

    /*public override Object? VisitBlockStmt([NotNull] lexicalAnalyzerParser.BlockStmtContext context){
        g.Comment("Visiting BlockStmt");
        g.newScope();
        foreach (var instruction in context.instruction()){
            Visit(instruction);
        }
        int bytesToRemove = g.endScope();
        if (bytesToRemove > 0){
            g.Comment("Removing " + bytesToRemove + " bytes from stack");
            g.Mov(Register.X0, bytesToRemove); 
            g.Add(Register.SP, Register.SP, Register.X0);
            g.Comment("Stack pointer adjusted to " + Register.SP);
        }
        return null;
    }*/

}