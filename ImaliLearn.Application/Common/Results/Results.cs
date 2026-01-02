namespace ImaliLearn.Application.Common.Results;

public class Result
{
    public bool IsSuccess { get; } // Indicates if the result is successful
    public string? Error { get; } // Contains error message if the result is a failure

    protected Result(bool isSuccess, string? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}
