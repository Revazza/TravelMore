using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Interfaces;

public interface IHotelPaymentCalculator<T>
{
    Result<T> Calculate();
}
