using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public class PremiumGuestPaymentCalculator : IHotelPaymentCalculator
{
    private readonly Hotel _hotel;

    private PremiumGuestPaymentCalculator(Hotel hotel)
    {
        _hotel = hotel;
    }

    public Money Calculate()
    {

        return Money.Create(0);
    }

    public static PremiumGuestPaymentCalculator Create(Hotel hotel) => new(hotel);
}
