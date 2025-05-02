 using System.Text;

public class StackObject{
    public enum StackObjectType{ Int, Float, String, Bool, Undefined}
    public StackObjectType Type { get; set; }
    public int Length { get; set; }
    public int Depth { get; set; }
    public string? Id { get; set; }
    public int Offset { get; set; }
}
public class ArmGenerator
{
    public  List<string> instructions = new List<string>();
    public  List<string> instructionsFunc = new List<string>();
    private readonly StandardLibrary stdLbr = new StandardLibrary();
    private List<StackObject> stackObjects = new List<StackObject>();
    private int depth = 0;
    private int labelCount = 0;
    public String GetLabel(){
        
        return $"L{labelCount++}";
    }
    public void SetLabel(string label){
        instructions.Add($"{label}:");
    }

    /* Stack Operations*/
    public StackObject PeekObject(){  //TopObject
        return stackObjects.Last();
    }
    public void PushObject(StackObject obj){
        Comment($"Pushing object {obj.Type} to stack");
        stackObjects.Add(obj);
        
    }

    public void PushConstant(StackObject obj, object value){
        switch (obj.Type){
            case StackObject.StackObjectType.Int:
                Mov(Register.X0, (int)value);
                Push(Register.X0);
                break;
            case StackObject.StackObjectType.Float:
                ulong floatb = (ulong)BitConverter.DoubleToInt64Bits((double)value);
                ushort[] floatP = new ushort[4];
                for (int i = 0; i < 4; i++){
                    floatP[i] = (ushort)((floatb >> (i * 16)) & 0xFFFF);
                    
                }
                instructions.Add($"MOVZ X0, #{floatP[0]}, LSL #0");
                for (int i = 1; i < 4; i++){
                    instructions.Add($"MOVK X0, #{floatP[i]}, LSL #{i * 16}");
                }
                Push(Register.X0);
                break;
            case StackObject.StackObjectType.Bool:
                Mov(Register.X0, (bool)value ? 1 : 0);
                Push(Register.X0);
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
        //PopObj();
        stackObjects.RemoveAt(stackObjects.Count - 1);
        Pop(rd);
        return obj;
    }
    public void PopObj(){
        Comment("Popping object from stack");
        try{
            stackObjects.RemoveAt(stackObjects.Count - 1);
        } catch (System.Exception){
            Console.WriteLine(this.ToString());
            throw new Exception("Stack underflow");
        }
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
    public StackObject BoolObject(){
        return new StackObject{
            Type = StackObject.StackObjectType.Bool,
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

    /* ==========  negate Operations   ========== */
    public void Neg(string rd, string rs){
        instructions.Add($"NEG {rd}, {rs}");
    }
    public void Mvn(string rd, string rs){
        instructions.Add($"MVN {rd}, {rs}");
    }
    public void Eor(string rd, string rs1){
        instructions.Add($"EOR {rd}, {rs1}, {"#1"}");
    }
    /* ==========  Relational Operations   ========== */
    public void Cmp(string rs1, string rs2){
        instructions.Add($"CMP {rs1}, {rs2}");
    }
    public void Fcmp(string rs1, string rs2){
        instructions.Add($"FCMP {rs1}, {rs2}");
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
    public void Bl(string label){
        instructions.Add($"BL {label}");
    }
    public void Cbz(string rs, string label){
        instructions.Add($"CBZ {rs}, {label}");
    }
    /*
    public void Label(string label){
        instructions.Add($"{label}:");
    }*/
    public void Add(string rd, string rs1, string rs2){
        instructions.Add($"ADD {rd}, {rs1}, {rs2}");

    }

    public void Sub(string rd, string rs1, string rs2){
        instructions.Add($"SUB {rd}, {rs1}, {rs2}");
    }
    public void Mul(string rd, string rs1, string rs2){
        instructions.Add($"MUL {rd}, {rs1}, {rs2}");
    }
    public void SDiv(string rd, string rs1, string rs2){
        instructions.Add($"SDIV {rd}, {rs1}, {rs2}");
    }
    public void Msub(string rd, string rs1, string rs2, string rs3){
        instructions.Add($"MSUB {rd}, {rs1}, {rs2}, {rs3}");
    }
    public void Addi(string rd, string rs1, int imm){
        instructions.Add($"ADDI {rd}, {rs1}, #{imm}");
    }

    public StackObject GetFrameLocal(int index){
        var obj = stackObjects.Where(o => o.Type == StackObject.StackObjectType.Undefined).ToList()[index];
        return obj;
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
    public void Br(string rs){
        instructions.Add($"BR {rs}");
    }
    //Operations Float

    public void Scvtf(string rd, string rs){
        instructions.Add($"SCVTF {rd}, {rs}");
    }
    public void Fmov(string rd, string rs){
        instructions.Add($"FMOV {rd}, {rs}");
    }
    public void Fadd(string rd, string rs1, string rs2){
        instructions.Add($"FADD {rd}, {rs1}, {rs2}");
    }
    public void Fsub(string rd, string rs1, string rs2){
        instructions.Add($"FSUB {rd}, {rs1}, {rs2}");
    }
    public void Fmul(string rd, string rs1, string rs2){
        instructions.Add($"FMUL {rd}, {rs1}, {rs2}");
    }
    public void Fdiv(string rd, string rs1, string rs2){
        instructions.Add($"FDIV {rd}, {rs1}, {rs2}");
    }
    public void Svc(){
        instructions.Add($"SVC #0");
    }
    public void Adr(string rd, string label){
        instructions.Add($"ADR {rd}, {label}");
    }

    public void NewLine(){

    }

    public void EndProgram(){
        /*mov x0, #1              // fd = 1 (stdout)
        adr x1, newline         // dirección del buffer con "\n"
        mov x2, #1              // longitud del carácter
        mov w8, #64             // syscall write
        svc #0*/

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
    public void  PrintStringSalto(string rs){
        stdLbr.Use("print_string");
        instructions.Add($"Mov X0, {rs}");
        instructions.Add($"BL print_string");
        instructions.Add($"Mov X0, #1");
        instructions.Add($"adr x1, newline");
        instructions.Add($"mov x2, #1");
        instructions.Add($"mov w8, #64");
        instructions.Add($"svc #0");
    }
    public void  PrintSalto(){
        
        instructions.Add($"Mov X0, #1");
        instructions.Add($"adr x1, newline");
        instructions.Add($"mov x2, #1");
        instructions.Add($"mov w8, #64");
        instructions.Add($"svc #0");
    }
    public void PrintFloat(){
        stdLbr.Use("print_integer");
        stdLbr.Use("print_double");
        instructions.Add($"BL print_double");
    }

    public void Comment(string comment){
        instructions.Add($"// {comment}");
    }
    public override string ToString(){
        var sb = new StringBuilder();
        //sb.AppendLine(".data");
        sb.AppendLine(".data");
        sb.AppendLine("newline: .ascii \"\\n\"");
        sb.AppendLine("heap: .space 4096");
        sb.AppendLine(".text");    
        sb.AppendLine(".global _start");
        sb.AppendLine("_start:");
        sb.AppendLine("adr x10, heap"); // Initialize HEAP pointer

        EndProgram();
        foreach (var instruction in instructions){
            sb.AppendLine(instruction);
        }
        sb.AppendLine("\n\n\n// Foreign Functions");
        instructionsFunc.ForEach(i => sb.AppendLine(i));
        sb.AppendLine("\n\n\n// Standard Library");
        //sb.AppendLine("\n\n // Library Functions");
        
        sb.AppendLine(stdLbr.GetFunctionDefinitions());
        return sb.ToString();
    }



}