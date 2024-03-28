using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.PremiumGuests;
using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Domain.Common.Factories;

public class HotelPaymentCalculatorFactory
{

    public static IHotelPaymentCalculator Create(
        short numberOfDays, 
        PaymentMethod paymentMethod,
        Guest guest,
        Hotel hotel)
    {
        return guest switch
        {
            StandardGuest => StandardGuestPaymentCalculator.Create(hotel, numberOfDays, Enums.PaymentMethod.Cash),
            PremiumGuest => PremiumGuestPaymentCalculator.Create(hotel, numberOfDays),
            _ => throw new Exception("Invalid guest type"),
        };
    }

}
