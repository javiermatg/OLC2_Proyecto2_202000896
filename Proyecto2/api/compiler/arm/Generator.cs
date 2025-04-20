 using System.Text;

public class StackObject{
    public enum StackObjectType{ Int, Float, String}
    public StackObjectType Type { get; set; }
    public int Length { get; set; }
    public int Depth { get; set; }
    public string? Id { get; set; }
}
public class ArmGenerator
{
    private readonly List<string> instructions = new List<string>();
    private readonly StandardLibrary stdLbr = new StandardLibrary();
    private List<StackObject> stackObjects = new List<StackObject>();
    private int depth = 0;

    /* Stack Operations*/
    public void PushObject(StackObject obj){
        stackObjects.Add(obj);
        
    }

    public void PushConstant(StackObject obj, object value){
        switch (obj.Type){
            case StackObject.StackObjectType.Int:
                Mov(Register.X0, (int)value);
                Push(Register.X0);
                break;
            case StackObject.StackObjectType.Float:
                // TODO:
                break;
            case StackObject.StackObjectType.String:
                List<byte> stringArray = Utilities.StringTo1ByteArray((string)value);
                Push(Register.HP);  
                for (int i = 0; i < stringArray.Count; i++){
                    var charCode = stringArray[i];
                    Comment($"Pushing char {charCode} to heap - ({(char)charCode})" );
                    Mov("w0", charCode);
                    Strb("w0", Register.HP);
                    Mov(Register.X0, 1);
                    Add(Register.HP, Register.HP, Register.X0);
                }
                break;
        }
        PushObject(obj);
    }

    public StackObject PopObject(string rd){
        var obj = stackObjects.Last();
        stackObjects.RemoveAt(stackObjects.Count - 1);
        Pop(rd);
        return obj;
    }

    public StackObject IntObject(){
        return new StackObject{
            Type = StackObject.StackObjectType.Int,
            Length = 8,
            Depth = depth,
            Id = null
        };
    }

    public StackObject FloatObject(){
        return new StackObject{
            Type = StackObject.StackObjectType.Float,
            Length = 8,
            Depth = depth,
            Id = null
        };
    }
    public StackObject StringObject(){
        return new StackObject{
            Type = StackObject.StackObjectType.String,
            Length = 8,
            Depth = depth,
            Id = null
        };
    }
    public StackObject CloneObject(StackObject obj){
        return new StackObject{
            Type = obj.Type,
            Length = obj.Length,
            Depth = obj.Depth,
            Id = obj.Id
        };
    }

    /*==== Environment OP =========*/
    public void newScope(){
        depth++;
    }

    public int endScope(){
        int byteOffset = 0;
        for (int i = stackObjects.Count - 1; i >= 0; i--){
           if (stackObjects[i].Depth == depth){
                byteOffset += stackObjects[i].Length;
                stackObjects.RemoveAt(i);
            } else {
                break;
            }
                   
        }
        depth--;
        return byteOffset;
    } 
        
    public void tagObject(string id){
        stackObjects.Last().Id = id;
    }
    public (int, StackObject) getObject(string id){
        int byteOffset = 0;
        for(int i = stackObjects.Count - 1; i >= 0; i--){
            if (stackObjects[i].Id == id){
                return (byteOffset, stackObjects[i]);
            }
            byteOffset += stackObjects[i].Length;
        }
        throw new Exception($"Object {id} not found");
    }
    /* ==================== */
    public void Cmp(string rs1, string rs2){
        instructions.Add($"CMP {rs1}, {rs2}");
    }
    public void Beq(string label){
        instructions.Add($"BEQ {label}");
    }
    public void Bne(string label){
        instructions.Add($"BNE {label}");
    }
    public void Bgt(string label){
        instructions.Add($"BGT {label}");
    }
    public void Blt(string label){
        instructions.Add($"BLT {label}");
    }
    public void Bge(string label){
        instructions.Add($"BGE {label}");
    }
    public void Ble(string label){
        instructions.Add($"BLE {label}");
    }
    public void B(string label){
        instructions.Add($"B {label}");
    }
    public void Label(string label){
        instructions.Add($"{label}:");
    }
    public void Add(string rd, string rs1, string rs2){
        instructions.Add($"ADD {rd}, {rs1}, {rs2}");

    }

    public void Sub(string rd, string rs1, string rs2){
        instructions.Add($"SUB {rd}, {rs1}, {rs2}");
    }
    public void Mul(string rd, string rs1, string rs2){
        instructions.Add($"MUL {rd}, {rs1}, {rs2}");
    }
    public void Div(string rd, string rs1, string rs2){
        instructions.Add($"DIV {rd}, {rs1}, {rs2}");
    }

    public void Addi(string rd, string rs1, int imm){
        instructions.Add($"ADDI {rd}, {rs1}, #{imm}");
    }

    // Store/Load instructions
    public void Str(string rd, string rs1, int offset = 0){
        instructions.Add($"STR {rd}, [{rs1}, #{offset}]");
    }

    public void Strb(string rs1, string rs2){
        instructions.Add($"STRB {rs1}, [{rs2}]");
    }
    public void Ldr(string rd, string rs1, int offset = 0){
        instructions.Add($"LDR {rd}, [{rs1}, #{offset}]");
    }

    public void Mov(string rd, int imm){
        instructions.Add($"MOV {rd}, #{imm}");
    }
    public void Push(string rs){
        instructions.Add($"STR {rs}, [sp, #-8]!");
    }
    public void Pop(string rs){
        instructions.Add($"LDR {rs}, [sp], #8");
    }
    public void Svc(){
        instructions.Add($"SVC #0");
    }

    public void EndProgram(){
        Mov(Register.X0, 0);
        Mov(Register.X8, 93); //syscall number for exit
        Svc(); //make syscall
    }

    public void PrintInt(string rs){
        stdLbr.Use("print_integer");        
        instructions.Add($"Mov X0, {rs}");
        instructions.Add($"BL print_integer");
    }
    public void  PrintString(string rs){
        stdLbr.Use("print_string");
        instructions.Add($"Mov X0, {rs}");
        instructions.Add($"BL print_string");
    }

    public void Comment(string comment){
        instructions.Add($"// {comment}");
    }
    public override string ToString(){
        var sb = new StringBuilder();
        //sb.AppendLine(".data");
        sb.AppendLine(".data");
        sb.AppendLine("heap: .space 4096");
        sb.AppendLine(".text");    
        sb.AppendLine(".global _start");
        sb.AppendLine("_start:");
        sb.AppendLine("adr x10, heap"); // Initialize HEAP pointer

        EndProgram();
        foreach (var instruction in instructions){
            sb.AppendLine(instruction);
        }
        sb.AppendLine("\n\n // Library Functions");
        
        sb.AppendLine(stdLbr.GetFunctionDefinitions());
        return sb.ToString();
    }



}