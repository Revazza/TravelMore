using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Coupons;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Memberships.Coupons;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Memberships;

public class PremiumMembership : Membership
{

    private PremiumMembership(Guid id) : base(id)
    {
        
    }

    private PremiumMembership(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<MembershipCoupon> membershipCoupons)
            : base(id, pricePerMonth, pricePerYear, guest, membershipCoupons)
    {

    }

    public static PremiumMembership Create(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<MembershipCoupon> membershipCoupons)
    {

        return new(id, pricePerMonth, pricePerYear, guest, membershipCoupons);
    }

}
