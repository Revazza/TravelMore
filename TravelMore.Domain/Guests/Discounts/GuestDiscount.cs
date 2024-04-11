using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Guests.Discounts;

public abstract class GuestDiscount : Discount<Guest, int>
{

    protected GuestDiscount(int id) : base(id)
    {
    }

}
