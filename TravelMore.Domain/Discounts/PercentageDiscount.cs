using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Discounts;

public class PercentageDiscount : Entity<Guid>, IDiscount
{
    public decimal DiscountPercentage { get; init; }
    public int GuestId { get; init; }
    public Guest Guest { get; init; } = null!;
    public int Limit { get; init; }
    public int TimesUsed { get; init; }
    public bool IsExpired => Limit == TimesUsed;


    public PercentageDiscount(Guid id, int limit, decimal discountPercentage, Guest guest) : base(id)
    {
        Limit = limit;
        Guest = guest;
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
