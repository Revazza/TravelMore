using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Guests.Discounts;

public abstract class GuestDiscount : BaseDiscount
{
    public int GuestId { get; protected set; }
    public Guest Guest { get; protected set; } = null!;


}
