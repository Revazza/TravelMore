using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;

namespace TravelMore.Domain.Discounts;

public abstract class BaseDiscount
{
    public Guid Id { get; set; }
    public DiscountType Type { get; init; }
    public Money Amount { get; init; } = 0;
    public virtual bool IsExpired { get; }

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
