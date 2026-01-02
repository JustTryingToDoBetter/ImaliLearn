namespace ImaliLearn.Application.Common.Results;

public class Result<T> : Result
{
    public T? Value { get; } // Contains the value if the result is successful

    // Constructor is private to control instantiation through static methods
    private Result(bool isSuccess, T? value, string? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, null); // Create a successful result with a value
    public static new Result<T> Failure(string error) => new(false, default, error); // Create a failure result with an error message
}
