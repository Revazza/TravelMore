using TravelMore.Domain.Common.Models;

namespace TravelMore.Application.Common.Results;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value)
        : base()
    {
        _value = value;
    }

    protected internal Result(Error error)
    : base(error)
    {

    }

    public TValue Value => _value!;

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);

}
