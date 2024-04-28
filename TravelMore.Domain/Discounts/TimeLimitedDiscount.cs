
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Discounts.Factories;

namespace TravelMore.Domain.Discounts;

public class TimeLimitedDiscount : Discount
{
    public DateTime ExpireDate { get; private set; }
    public override bool IsExpired => DateTime.UtcNow >= ExpireDate;

    private TimeLimitedDiscount(
        Guid id,
        DateTime expireDate,
        DiscountType type,
        DiscountSubject subject,
        Money value) : base(id, type, subject, value)
    {
        ExpireDate = expireDate;
    }

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        return DiscountStrategyFactory.Create(price, Value, Type).Apply();
    }

    public override TimeLimitedDiscount Clone()
    {
        return new(Id, ExpireDate, Type, Subject, Value);
    }
}
