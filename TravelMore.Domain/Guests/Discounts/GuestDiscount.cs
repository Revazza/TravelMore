using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Guests.Discounts;

public class GuestDiscount : Entity<Guid>
{
    public int GuestId { get; protected set; }
    public Guest Guest { get; protected set; } = null!;
    public Guid DiscountId { get; set; }
    public Discount Discount { get; set; } = null!;

    public GuestDiscount(Guid id) : base(id)
    {
    }

}
