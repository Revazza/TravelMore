using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Discounts.Factories;

namespace TravelMore.Domain.Discounts;

public class LimitedUseDiscount : Discount
{
    public int RemainingUses { get; protected set; }
    public override bool IsExpired => RemainingUses == 0;

    private LimitedUseDiscount() : base() { }

    private LimitedUseDiscount(
        Guid id,
        int remainingUses,
        DiscountType type,
        DiscountSubject subject,
        Money value) : base(id, type, subject, value)
    {
        RemainingUses = remainingUses;
    }

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        RemainingUses--;
        return DiscountStrategyFactory.Create(price, Value, Type).Apply();
    }

    public override LimitedUseDiscount Clone() => new(Id, RemainingUses, Type, Subject, Value);
}
