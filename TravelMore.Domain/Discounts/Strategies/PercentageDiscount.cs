using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts.Strategies;

public class PercentageDiscount(Money originalPrice, Money percentage) : IDiscountStrategy
{
    private readonly Money _percentage = percentage;
    private readonly Money _originalPrice = originalPrice;

    public Money Apply()
    {
        var discountAmount = _originalPrice * _percentage;
        var discountedPrice = _originalPrice - discountAmount;
        return discountedPrice;
    }

}
