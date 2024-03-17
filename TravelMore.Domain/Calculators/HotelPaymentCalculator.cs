using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public class HotelPaymentCalculator(Hotel hotel, short numberOfGuests) : IHotelPaymentCalculator<Money>
{
    private readonly Hotel _hotel = hotel;
    private readonly short _numberOfGuests = numberOfGuests;

    public Result<Money> Calculate() => Money.Create(_hotel.PricePerNight.Amount * _numberOfGuests);

}
