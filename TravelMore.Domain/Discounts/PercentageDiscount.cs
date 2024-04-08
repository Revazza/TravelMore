using System.Diagnostics;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Users;

namespace TravelMore.Domain.Discounts;

public class PercentageDiscount : Entity<Guid>, IDiscount
{
    public decimal DiscountPercentage { get; init; }
    public int UserId { get; init; }
    public User User { get; init; } = null!;
    public int Limit { get; init; }
    public int TimesUsed { get; init; }
    public bool IsExpired => Limit == TimesUsed;


    public PercentageDiscount(Guid id, int limit, decimal discountPercentage, User user) : base(id)
    {
        Limit = limit;
        User = user;
        DiscountPercentage = discountPercentage;
    }

    private PercentageDiscount(Guid id) : base(id)
    {
    }

    public Money Apply(Money price)
        => price.Amount - CalculateDiscountAmount(price);

    private Money CalculateDiscountAmount(Money price)
        => price.Amount * (DiscountPercentage / 100);

}
