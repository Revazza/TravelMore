namespace TravelMore.Domain.Common.Models;

using TravelMore.Domain.Common.Exceptions;
using TravelMore.Domain.Common.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

public record Money
{
    public decimal Amount { get; private set; }

    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Money Create(decimal amount)
    {
        EnsureNonNegativeAmount(amount);
        return new(amount);
    }

    public static bool operator >(Money left, Money right) => left.Amount > right.Amount;

    public static bool operator <(Money left, Money right) => left.Amount < right.Amount;

    public static bool operator >=(Money left, Money right) => left.Amount >= right.Amount;

    public static bool operator <=(Money left, Money right) => left.Amount <= right.Amount;

    public static implicit operator Money(decimal amount) => Create(amount);

    private static void EnsureNonNegativeAmount(decimal amount)
    {
        if (amount.IsNegative())
        {
            throw new NegativeAmountException();
        }
    }

}


