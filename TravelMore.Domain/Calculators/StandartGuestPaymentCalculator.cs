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
    private readonly short _numberOfDays;
    private const decimal FeePercentage = 0.15m;

    private StandartGuestPaymentCalculator(Hotel hotel, short numberOfGuests, short numberOfDays)
    {
        _hotel = hotel;
        _numberOfGuests = numberOfGuests;
        _numberOfDays = numberOfDays;
    }

    public static StandartGuestPaymentCalculator Create(Hotel hotel, short numberOfGuests, short numberOfDays)
    {
        if (numberOfGuests.IsLessThanOrEqualToZero())
        {
            throw new HotelInvalidGuestNumberException();
        }
        return new(hotel, numberOfDays, numberOfGuests);
    }

    public Money Calculate()
    {
        var payment = CalculatePayment();
        var totalPayment = CalculateTotalPayment(payment);
        return totalPayment;
    }

    private decimal CalculatePayment() => _hotel.PricePerDay.Amount * _numberOfGuests * _numberOfDays;

    private static decimal CalculateTotalPayment(decimal payment) => payment + CalculateFee(payment);

    private static decimal CalculateFee(decimal payment) => payment * FeePercentage;

}
