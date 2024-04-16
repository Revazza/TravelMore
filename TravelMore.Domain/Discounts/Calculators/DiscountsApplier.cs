using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts.Calculators;

public class DiscountsApplier
{
    private readonly IReadOnlyCollection<Discount> _discounts;

    public DiscountsApplier(List<Discount> discounts)
    {
        _discounts = discounts;
    }

    public Money Apply(Money price) => _discounts.Aggregate(price, (current, discount) => current - discount.Apply(price));
}
