using TravelMore.Domain.Common.Intefaces;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;

namespace TravelMore.Domain.Discounts;

public abstract class Discount : Entity<Guid>, ICloneable<Discount>
{
    protected Discount() : base(Guid.NewGuid())
    {
    }

    protected Discount(Guid id, DiscountType type, DiscountSubject subject, Money value) : base(id)
    {
        Type = type;
        Subject = subject;
        Value = value;
    }

    public DiscountType Type { get; init; }
    public DiscountSubject Subject { get; init; }
    public Money Value { get; init; } = 0;
    public virtual bool IsExpired { get; }

    public abstract Money Apply(Money price);

    public abstract Discount Clone();

    protected virtual void EnsureNotExpired()
    {
        // Todo: Create custom exception
        if (IsExpired)
        {
            throw new Exception("Discount expired");
        }
    }

}
