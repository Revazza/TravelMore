using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public class HotelPaymentCalculator : IHotelPaymentCalculator<Money>
{
    private readonly Hotel _hotel;
    private readonly short _numberOfGuests;

    private HotelPaymentCalculator(Hotel hotel, short numberOfGuests)
    {
        _hotel = hotel;
        _numberOfGuests = numberOfGuests;
    }

    public static HotelPaymentCalculator Create(Hotel hotel, short numberOfGuests)
    {
        if (numberOfGuests.IsLessThanOrEqualToZero())
        {
            throw new HotelInvalidGuestNumberException();
        }

        return new(hotel, numberOfGuests);
    }

    public Money Calculate() => Money.Create(_hotel.PricePerNight.Amount * _numberOfGuests);

}
