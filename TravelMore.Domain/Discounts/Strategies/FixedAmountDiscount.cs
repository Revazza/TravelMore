using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts.Strategies;

public class FixedAmountDiscount(Money price, Money discount) : IDiscountStrategy
{
    private readonly Money _discount = discount;
    private readonly Money _price = price;

    public Money Apply()
    {
        var discountedPrice = _price.Amount - _discount.Amount;

        if (discountedPrice.IsLessThanOrEqualToZero())
        {
            return 0;
        }

        return discountedPrice;
    }

}
