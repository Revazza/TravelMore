using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts;

public abstract class PercentageDiscount<TEntity, TId> : Discount<TEntity, TId>
    where TEntity : Entity<TId>
{
    public decimal DiscountPercentage { get; init; }
    public int Limit { get; init; }
    public int TimesUsed { get; init; }
    public override bool IsExpired => Limit == TimesUsed;

    protected PercentageDiscount(TId id) : base(id)
    {
    }

}
