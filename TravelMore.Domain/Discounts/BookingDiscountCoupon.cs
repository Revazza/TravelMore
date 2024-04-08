using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Discounts.ValueObjects;
using TravelMore.Domain.Memberships;
using TravelMore.Domain.Users;

namespace TravelMore.Domain.Discounts;

public class BookingDiscountCoupon : Entity<Guid>, IDiscount
{
    public CouponCode Code { get; set; }
    public Money DiscountAmount { get; set; }
    public Guid MembershipId { get; init; }
    public Membership Membership { get; init; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsExpired => DateTime.UtcNow <= ExpiryDate;

    private BookingDiscountCoupon(Guid id) : base(id)
    {
    }

    public Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
