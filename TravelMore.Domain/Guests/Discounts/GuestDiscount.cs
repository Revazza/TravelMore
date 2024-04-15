using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Guests.Discounts;

public class GuestDiscount : Discount
{
    public int GuestId { get; protected set; }
    public Guest Guest { get; protected set; } = null!;

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
