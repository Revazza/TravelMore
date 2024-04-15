
namespace TravelMore.Domain.Discounts;

public abstract class LimitedUseDiscount : Discount
{
    public int RemainingUses { get; protected set; }
    public override bool IsExpired => RemainingUses == 0;

}
