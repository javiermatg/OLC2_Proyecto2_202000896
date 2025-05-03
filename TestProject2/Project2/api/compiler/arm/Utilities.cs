public static class Utilities{
    public static List<byte> StringTo1ByteArray(string str){
        var resultado = new List<byte>();
        int elementIndex = 0;
        while(elementIndex < str.Length){
            resultado.Add((byte)str[elementIndex]);
            elementIndex++;
        }
        resultado.Add(0); // Agregar el terminador nulo
        return resultado;
    }
}