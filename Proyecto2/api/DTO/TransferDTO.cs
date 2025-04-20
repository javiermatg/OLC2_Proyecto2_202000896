public class BreakException : Exception
{
    public BreakException() : base("Break statement")
    {
    }
}

public class ContinueException : Exception
{
    public ContinueException() : base("Continue statement")
    {
    }
}
public class ReturnException : Exception
{
    public valueContentDTO Value { get; }

    public ReturnException(valueContentDTO value) : base("Return statement")
    {   
        Console.WriteLine("Return value: " + value);
        Value = value;
    }
}