using TravelMore.Domain.Coupons;

namespace TravelMore.Domain.Memberships.Coupons;

public class MembershipCoupon : Coupon<Membership, Guid>
{
    public MembershipCoupon(Guid id) : base(id)
    {
    }
}
