using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Factories;

namespace TravelMore.Domain.Discounts;

public class LimitedUseDiscount : Discount
{
    public int RemainingUses { get; protected set; }
    public override bool IsExpired => RemainingUses == 0;

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        RemainingUses--;
        return DiscountStrategyFactory.Create(price, Value, Type).Apply();
    }
}
