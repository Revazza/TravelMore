namespace TravelMore.Domain.Common.Models;

using TravelMore.Domain.Common.Extensions;
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
        if (amount.IsNegative())
        {
            return Errors.DomainErrors.Money.InvalidAmount;
        }

        return new Money(amount);
    }

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;
}


