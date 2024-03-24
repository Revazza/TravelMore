using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public class StandartGuestPaymentCalculator : IHotelPaymentCalculator
{
    private readonly Hotel _hotel;
    private readonly short _numberOfGuests;
    private const decimal FeePercentage = 0.15m;

    private StandartGuestPaymentCalculator(Hotel hotel, short numberOfGuests)
    {
        _hotel = hotel;
        _numberOfGuests = numberOfGuests;
    }

    public static StandartGuestPaymentCalculator Create(Hotel hotel, short numberOfGuests)
    {
        if (numberOfGuests.IsLessThanOrEqualToZero())
        {
            throw new HotelInvalidGuestNumberException();
        }

        return new(hotel, numberOfGuests);
    }

    public Money Calculate()
    {
        var payment = _hotel.PricePerNight.Amount * _numberOfGuests;
        var fee = payment * FeePercentage;
        var totalPayment = payment * fee;
        return Money.Create(totalPayment);
    }

}
