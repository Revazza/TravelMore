using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Discounts.Calculators;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Bookings.Calculators;

public class BookingPaymentCalculator
{
    private readonly Guest _guest;
    private readonly Hotel _hotel;
    private readonly List<Discount> _discounts;

    public BookingPaymentCalculator(Guest guest, Hotel hotel, List<Discount> discounts)
    {
        _guest = guest;
        _hotel = hotel;
        _discounts = discounts;
    }

    public void Calculate()
    {
        var price = Money.Default;
        var discountedPrice = DiscountsApplier.Apply(price, _discounts);
        var discountedAmount = price - discountedPrice;

    }

}
