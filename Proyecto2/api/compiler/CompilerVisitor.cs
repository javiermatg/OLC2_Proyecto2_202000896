
using System;
using System.Net;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;


public class CompilerVisitor : lexicalAnalyzerBaseVisitor<Object?>
{   
    //public Dictionary<string, SymbolsDTO> Table = new Dictionary<string, SymbolsDTO>();
    //public List<TableSymbol> Table = new List<TableSymbol>();
   
    public ArmGenerator g = new ArmGenerator();
    public int contadorIf = 0;
    public CompilerVisitor() {
        
        
    }
    public override Object? VisitInit([NotNull] lexicalAnalyzerParser.InitContext context){
        foreach (var instruction in context.instruction()){
            Visit(instruction);
        }
        return null;
    }
    
    /*public override Object? VisitInstruction([NotNull] lexicalAnalyzerParser.InstructionContext context){
        return null;
    }*/
    public override Object? VisitPrintVar([NotNull] lexicalAnalyzerParser.PrintVarContext context){
        g.Comment("Print Expr");
        g.Comment("Visiting Expr");
        foreach(lexicalAnalyzerParser.ExprContext expr in context.expr()){
            Visit(expr);
            g.Comment("Popping value to print");
            var value = g.PopObject(Register.X0); //pop the value to print
            if (value.Type == StackObject.StackObjectType.Int){
                g.PrintInt(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Float){
                //g.PrintFloat(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.String){
                g.PrintString(Register.X0);
            } else if (value.Type == StackObject.StackObjectType.Bool){
                g.PrintInt(Register.X0);
            } /*else if (value.Type == StackObject.StackObjectType.Null){
                g.PrintNull(Register.X0);
            }*/

        }
        //Visit(context.expr());
        return null;
    }
    public override Object? VisitStmtVar([NotNull] lexicalAnalyzerParser.StmtVarContext context){
        var name = context.ID().GetText();
        g.Comment("Visiting StmtVar: " + name);
        Visit(context.expr());
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
        Visit(context.exprIf);
        g.Comment("Visiting IfStmt");
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
        contadorIf++;
        return null;
    }
    public override Object? VisitFuncStmt([NotNull] lexicalAnalyzerParser.FuncStmtContext context){
        return null;
    }
    public override Object? VisitFunctionCall([NotNull] lexicalAnalyzerParser.FunctionCallContext context){
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
        return null;
    }
    public override Object? VisitSwitchStmt([NotNull] lexicalAnalyzerParser.SwitchStmtContext context){
        return null;
    }
    public override Object? VisitReturnStmt([NotNull] lexicalAnalyzerParser.ReturnStmtContext context){
        return null;
    }
    public override Object? VisitBreakTransfer([NotNull] lexicalAnalyzerParser.BreakTransferContext context){
        return null;
    }
    public override Object? VisitContinueTransfer([NotNull] lexicalAnalyzerParser.ContinueTransferContext context){
        return null;
    }
    public override Object? VisitAddSub([NotNull]lexicalAnalyzerParser.AddSubContext context){
        g.Comment("Visiting AddSub");
        var op = context.GetChild(1).GetText();

        g.Comment("Visiting Expr left");
        Visit(context.expr(0));
        g.Comment("Visiting Expr right");
        Visit(context.expr(1));

        g.Comment("Popping values");
        var right = g.PopObject(Register.X1); // Ya se tiene el tipo de valor a operar
        var left = g.PopObject(Register.X0);
        if (op == "+"){
            g.Add(Register.X0, Register.X0, Register.X1);
        } else if (op == "-"){
            g.Sub(Register.X0, Register.X0, Register.X1);
        }
        g.Comment("Pushing Result");
        g.Push(Register.X0);
        g.PushObject(g.CloneObject(left)); // se clona el valor con el tipo que tiene más predominancia
        return null;
    }
    public override Object? VisitMulDiv([NotNull]lexicalAnalyzerParser.MulDivContext context){
        return null;
    }
    public override Object? VisitModule([NotNull] lexicalAnalyzerParser.ModuleContext context){
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
        var right = g.PopObject(Register.X1); // Ya se tiene el tipo de valor a operar
        var left = g.PopObject(Register.X0);
        g.Comment("Comparing values");
        g.Cmp(Register.X0, Register.X1);
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
        }
        return new BoolValue(true);
        //return null;
        
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
        g.PushConstant(floatObject, float.Parse(value));

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
        g.PushConstant(boolObject, bool.Parse(value));
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

    public override Object? VisitBlockStmt([NotNull] lexicalAnalyzerParser.BlockStmtContext context){
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
    }
}