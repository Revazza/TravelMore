using TravelMore.Domain.Memberships;

namespace TravelMore.Domain.Coupons.MembershipCoupons;

public class MembershipCoupon : Coupon<Membership, Guid>
{
    public MembershipCoupon(Guid id) : base(id)
    {
    }
}
