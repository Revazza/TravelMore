using System.Diagnostics;
using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts.Extensions;

public static class DiscountsExtensions
{

    public static Money ApplyAll(this IEnumerable<Discount> discounts, Money price)
        => discounts.Aggregate(price, (currentPrice, discount) => discount.Apply(currentPrice));
}
