
using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Interfaces;

public interface IHotelPaymentCalculator
{
    Money Calculate();
}
