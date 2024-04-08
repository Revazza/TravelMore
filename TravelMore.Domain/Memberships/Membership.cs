using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Memberships;

public abstract class Membership : Entity<Guid>
{
    protected readonly List<IDiscount> _discounts = [];
    public Money PricePerMonth { get; set; } = 0;
    public Money PricePerYear { get; set; } = 0;
    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;
    public IReadOnlyCollection<IDiscount> Discounts => _discounts;

    protected Membership(
        Guid id,
        Money pricePerMonth, 
        Money pricePerYear, 
        Guest guest, 
        List<IDiscount> discounts) : base(id)
    {
        PricePerMonth = pricePerMonth;
        PricePerYear = pricePerYear;
        Guest = guest;
        _discounts = discounts;
    }

    private Membership(Guid id) : base(id)
    {
    }

}
