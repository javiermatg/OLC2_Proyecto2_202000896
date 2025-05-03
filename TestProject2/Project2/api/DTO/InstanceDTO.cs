public class Instance {
    private LenguageClass struc;
    private Dictionary<string, valueContentDTO> Properties;

    public Instance(LenguageClass struc){
        this.struc = struc;
        Properties = new Dictionary<string, valueContentDTO>();
    }

    public void Set(string id, valueContentDTO value){
        Properties[id] = value;
    }

    public valueContentDTO Get(string id){
        if (Properties.ContainsKey(id)){
            return Properties[id];
        }

        var method = struc.getMethod(id);
        if (method != null){
            return new FunctionValue(method.Bind(this), id);
        }

        throw new SemanticError("Property not found", null);
    }
}