﻿
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Factories;

namespace TravelMore.Domain.Discounts;

public class TimeLimitedDiscount : Discount
{
    public DateTime ExpireDate { get; private set; }
    public override bool IsExpired => DateTime.UtcNow >= ExpireDate;

    public override Money Apply(Money price)
    {
        EnsureNotExpired();
        return DiscountStrategyFactory.Create(price, Value, Type).Apply();
    }
}
