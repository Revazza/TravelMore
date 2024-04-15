
using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts;

public class LimitedUseDiscount : Discount
{
    public int RemainingUses { get; protected set; }
    public override bool IsExpired => RemainingUses == 0;

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
