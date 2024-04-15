using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;

namespace TravelMore.Domain.Guests.Discounts;

public class GuestBookingDiscount : GuestDiscount
{
    private readonly int _limit;
    public int TimesUsed { get; private set; }
    public override bool IsExpired => _limit == TimesUsed;

    private GuestBookingDiscount(int id)
    {
    }

    public GuestBookingDiscount(int limit, DiscountType type, Money amount, Guest guest)
    {
        _limit = limit;
        Type = type;
        Amount = amount;
        Guest = guest;
    }

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        TimesUsed++;
        return price;
    }


}
