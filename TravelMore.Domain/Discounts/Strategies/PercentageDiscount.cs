using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts.Strategies;

public class PercentageDiscount(Money price, Money percentage) : IDiscountStrategy
{
    private readonly Money _percentage = percentage;
    private readonly Money _price = price;

    public Money Apply() => _price * (_percentage / 100);

}
