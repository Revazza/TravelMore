using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts.Strategies;

public class FixedAmountDiscount(Money amount) : IDiscountStrategy
{
    private readonly Money _amount = amount;

    public Money Apply(Money amount) => amount - _amount;

}
