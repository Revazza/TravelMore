using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Coupons;
using TravelMore.Domain.Coupons.MembershipCoupons;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Memberships;

public abstract class Membership : Entity<Guid>
{
    protected readonly List<MembershipCoupon> _coupons = [];
    public Money PricePerMonth { get; set; } = 0;
    public Money PricePerYear { get; set; } = 0;
    public int GuestId { get; set; }
    public Guest Guest { get; set; } = null!;
    public IReadOnlyCollection<MembershipCoupon> Coupons => _coupons;

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
