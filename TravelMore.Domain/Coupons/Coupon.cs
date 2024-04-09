using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Coupons.Interfaces;
using TravelMore.Domain.Coupons.ValueObjects;

namespace TravelMore.Domain.Coupons;

public abstract class Coupon<TEntity, TId> 
    : Entity<Guid>, ICoupon
    where TEntity : Entity<TId>
{
    public CouponCode Code { get; set; } = CouponCode.Create(string.Empty);
    public Money DiscountAmount { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsExpired => DateTime.UtcNow <= ExpiryDate;
    public TId TargetId { get; set; }
    public TEntity Target { get; set; }

    protected Coupon(Guid id) : base(id)
    {
    }
}
