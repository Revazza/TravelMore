using TravelMore.Domain.Calculators;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.PremiumGuests;
using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Domain.Common.Factories;

public class HotelPaymentCalculatorFactory
{

    public static IHotelPaymentCalculator Create(Guest guest, Hotel hotel, short numberOfGuests, short numberOfNights)
    {
        return guest switch
        {
            StandardGuest => StandardGuestPaymentCalculator.Create(hotel, numberOfGuests,Enums.PaymentMethod.Cash),
            PremiumGuest => PremiumGuestPaymentCalculator.Create(hotel, numberOfNights),
            _ => throw new Exception("Invalid guest type"),
        };
    }

}
