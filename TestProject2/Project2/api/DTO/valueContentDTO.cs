public abstract record valueContentDTO;

public record IntValue(int Value) : valueContentDTO;
public record FloatValue(float Value) : valueContentDTO;
public record StringValue(string Value) : valueContentDTO;
public record BoolValue(bool Value) : valueContentDTO;



public record FunctionValue(Invocable invocable, string name) : valueContentDTO;
public record InstanceValue(Instance instance) : valueContentDTO;

public record LenguageValue(LenguageClass struc) : valueContentDTO;

public record VoidValue : valueContentDTO;


// public class ValueWp
// {

//     public enum ValueType
//     {
//         Int,
//         Float,
//         String,
//         Bool,
//         Void
//     }

//     public ValueType Type { get; }
//     public object Value { get; }
// }
