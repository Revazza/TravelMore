
namespace TravelMore.Domain.Common.Results;

public class Result<TValue>
    where TValue : class
{
    public TValue Value { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    private Result(TValue payload)
    {
        Value = payload;
        IsSuccess = true;
        Error = Error.None;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
        Value = null!;
    }


    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);
}
