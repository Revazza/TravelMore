using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Guests.Discounts;

public class GuestOneTimeBookingDiscount : GuestDiscount
{
    public Money Discount { get; init; } = 0;

    public bool IsUsed { get; set; }

    public override bool IsExpired => IsUsed;

    public GuestOneTimeBookingDiscount(int id) : base(id)
    {
    }

    public override Money Apply(Money price)
    {
        IsUsed = true;
        return price - Discount;
    }
}
