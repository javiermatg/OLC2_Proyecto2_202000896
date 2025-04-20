public interface Invocable
{
    int Arity();
    valueContentDTO Invoke(List<valueContentDTO> args, InterpreterVisitor visitor);
}