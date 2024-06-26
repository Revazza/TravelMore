﻿using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Discounts.Exceptions;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Discounts.Strategies;

namespace TravelMore.Domain.Discounts.Factories;

public class DiscountStrategyFactory
{

    public static IDiscountStrategy Create(Money price, Money value, DiscountType discountType)
    {
        return discountType switch
        {
            DiscountType.FixedPrice => new FixedAmountDiscount(price, value),
            DiscountType.Percentage => new PercentageDiscount(price, value),
            _ => throw new DiscountStrategyCreationException(),
        };
    }

}
