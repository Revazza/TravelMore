using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Guests.Discounts;

public class GuestBookingDiscount : GuestDiscount
{
    public Money Discount { get; init; } = 0;
    public int Limit { get; init; }
    public int TimesUsed { get; private set; }
    public override bool IsExpired => Limit == TimesUsed;

    public GuestBookingDiscount(int id) : base(id)
    {
    }

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        TimesUsed++;
        return price - Discount;
    }

    private void EnsureNotExpired()
    {
        // Todo: Create custom exception
        if (IsExpired)
        {
            throw new Exception("Discount expired");
        }
    }

}
