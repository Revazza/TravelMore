using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts.Calculators;

public class DiscountsApplier
{
    public static Money Apply(Money price, IReadOnlyCollection<Discount> discounts)
        => discounts.Aggregate(price, (current, discount) => current - discount.Apply(price));
}
