
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelMore.Domain.Common.Results;

public class Result
{
    protected internal Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    protected internal Result()
    {
        IsSuccess = true;
        Error = Error.None;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public static Result Success() => new();

    public static Result<TValue> Success<TValue>(TValue value) => new(value);

    public static Result Failure(Error error) => new(error);

    public static Result<TValue> Failure<TValue>(Error error) => new(error);

    public static implicit operator Result(Error error) => new(error);

}
