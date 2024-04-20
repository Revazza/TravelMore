using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Discounts.Interfaces;

public interface IDiscountStrategy
{
    Money Apply();
}
