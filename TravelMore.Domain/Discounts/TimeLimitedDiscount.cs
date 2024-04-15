
namespace TravelMore.Domain.Discounts;

public abstract class TimeLimitedDiscount : Discount
{
    public DateTime ExpireDate { get; private set; }
    public override bool IsExpired => DateTime.UtcNow >= ExpireDate;

}
