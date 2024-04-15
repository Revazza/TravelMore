using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;

namespace TravelMore.Domain.Discounts;

public class Discount : Entity<Guid>
{
    public DiscountType Type { get; init; }
    public DiscountSubject Subject { get; init; }
    public Money Amount { get; init; } = 0;
    public virtual bool IsExpired { get; }

    protected Discount() : base(Guid.NewGuid())
    {
    }

    public virtual Money Apply(Money price)
    {
        return 0;
    }

    protected virtual void EnsureNotExpired()
    {
        // Todo: Create custom exception
        if (IsExpired)
        {
            throw new Exception("Discount expired");
        }
    }

}
