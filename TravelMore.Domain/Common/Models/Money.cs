namespace TravelMore.Domain.Common.Models;

using TravelMore.Domain.Common.Results;

public record Money
{
    public decimal Amount { get; private set; }

    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Result<Money> Create(decimal amount)
    {
        if (IsNegative(amount))
        {
            return Result.Failure<Money>(Errors.DomainErrors.Money.InvalidAmount);
        }

        return Result.Success(new Money(amount));
    }

    private static bool IsNegative(decimal amonut) => amonut < 0;

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;
}


