
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Memberships.Coupons;

namespace TravelMore.Domain.Memberships;

public class StandardMembership : Membership
{
    private StandardMembership(Guid id) : base(id)
    {

    }

    private StandardMembership(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<MembershipCoupon> membershipCoupons)
            : base(id, pricePerMonth, pricePerYear, guest, membershipCoupons)
    {

    }

    public static StandardMembership Create(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<MembershipCoupon> membershipCoupons)
    {

        return new(id, pricePerMonth, pricePerYear, guest, membershipCoupons);
    }

}
