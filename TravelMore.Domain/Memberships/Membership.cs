using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Memberships.Coupons;
using TravelMore.Domain.Memberships.Discounts;

namespace TravelMore.Domain.Memberships;

public abstract class Membership : Entity<Guid>
{
    protected readonly List<MembershipCoupon> _coupons = [];
    protected readonly List<MembershipDiscount> _discounts = [];
    public Money PricePerMonth { get; set; } = 0;
    public Money PricePerYear { get; set; } = 0;
    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;
    public IReadOnlyCollection<MembershipCoupon> Coupons => _coupons;
    public IReadOnlyCollection<MembershipDiscount> Discounts => _discounts;

    protected Membership(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<MembershipCoupon> coupons) : base(id)
    {
        PricePerMonth = pricePerMonth;
        PricePerYear = pricePerYear;
        Guest = guest;
        _coupons = coupons;
    }

    protected Membership(Guid id) : base(id)
    {
    }

}
