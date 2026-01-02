



namespace ImaliLearn.API.Models;

public class ErrorResponse
{
    public string Code {get; init;} = string.Empty; 

    public string Message {get; init;} = string.Empty;

    public int Status {get; init;}

    public string? TraceId {get; init;}

    public IDictionary<string, string[]>? Details {get; init;}

}