using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Coupons.ValueObjects;

namespace TravelMore.Domain.Coupons.Interfaces;

public interface ICoupon
{
    public CouponCode Code { get; protected set; }
    public Money DiscountAmount { get; protected set; }
    public bool IsExpired { get; }
}
