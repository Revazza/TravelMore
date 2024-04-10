using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;

namespace TravelMore.Domain.Discounts;

public abstract class Discount<TEntity, TId> : Entity<TId>, IDiscount
    where TEntity : Entity<TId>
{
    public TId TargetId { get; private set; }
    public TEntity Target { get; private set; } = null!;

    public virtual bool IsExpired { get; }

    protected Discount(TId id) : base(id)
    {
    }

    public abstract Money Apply(Money price);

}
