using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Guests;

namespace TravelMore.Domain.Discounts;

public abstract class Discount : Entity<Guid>
{
    public DiscountType Type { get; init; }
    public DiscountSubject Subject { get; init; }
    public Money Amount { get; init; } = 0;
    public virtual bool IsExpired { get; }

    protected Discount() : base(Guid.NewGuid())
    {
    }

    public abstract Money Apply(Money price);

    protected virtual void EnsureNotExpired()
    {
        // Todo: Create custom exception
        if (IsExpired)
        {
            throw new Exception("Discount expired");
        }
    }

}
