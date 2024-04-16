using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Memberships.Coupons;

namespace TravelMore.Domain.Memberships;

public class Membership : Entity<Guid>
{
    protected readonly List<MembershipCoupon> _coupons = [];
    protected readonly List<Discount> _discounts = [];
    public Money PricePerMonth { get; set; } = 0;
    public Money PricePerYear { get; set; } = 0;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;
    public IReadOnlyCollection<MembershipCoupon> Coupons => _coupons;
    public IReadOnlyCollection<Discount> Discounts => _discounts;


    protected Membership() : base(Guid.NewGuid())
    {
    }

    protected Membership(Guest guest) : base(Guid.NewGuid())
    {
    }

    public bool HasDiscount(Discount discount) => Discounts.Contains(discount);

}
