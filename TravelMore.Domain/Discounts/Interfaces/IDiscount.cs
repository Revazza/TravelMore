using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts.Interfaces;

public interface IDiscount
{
    bool IsExpired { get; }
    public Money Apply(Money price);
}
