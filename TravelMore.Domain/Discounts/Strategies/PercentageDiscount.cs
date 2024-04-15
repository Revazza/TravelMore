using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts.Strategies;

public class PercentageDiscount(Money percentage) : IDiscountStrategy
{
    private readonly Money _percentage = percentage;

    public Money Apply(Money amount) => amount * (_percentage / 100);

}
