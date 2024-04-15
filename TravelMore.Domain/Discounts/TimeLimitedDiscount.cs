
using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts;

public class TimeLimitedDiscount : Discount
{
    public DateTime ExpireDate { get; private set; }
    public override bool IsExpired => DateTime.UtcNow >= ExpireDate;

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
