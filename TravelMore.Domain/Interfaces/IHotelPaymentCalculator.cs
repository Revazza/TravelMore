
namespace TravelMore.Domain.Interfaces;

public interface IHotelPaymentCalculator<T>
{
    T Calculate();
}
