public class ErrorsDTO{
    public string errorType { get; set; }
    public string description { get; set; }
    public int line { get; set; }
    public int column { get; set; }

    public ErrorsDTO(string errorType, string description, int line, int column){
        this.errorType = errorType;
        this.description = description;
        this.line = line;
        this.column = column;
    }
}