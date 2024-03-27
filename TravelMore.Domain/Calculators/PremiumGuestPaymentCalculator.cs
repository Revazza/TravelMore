using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public class PremiumGuestPaymentCalculator : IHotelPaymentCalculator
{
    protected readonly Hotel _hotel;
    protected readonly short _numberOfNights;

    private PremiumGuestPaymentCalculator(Hotel hotel, short numberOfNights)
    {
        _hotel = hotel;
        _numberOfNights = numberOfNights;
    }

    public Money Calculate() => CalculateTotalPayment();

    public static PremiumGuestPaymentCalculator Create(Hotel hotel, short numberOfDays) => new(hotel, numberOfDays);

    private decimal CalculateTotalPayment() => _hotel.PricePerDay.Amount * _numberOfNights;

}
