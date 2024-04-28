using System.Security.Cryptography;
using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;

namespace TravelMore.Domain.Bookings;

public class DraftBooking : Booking
{
    public PriceDetails PriceDetails { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public DateTime CreatedAt { get; init; }

    private DraftBooking(
        PaymentMethod paymentMethod,
        PriceDetails priceDetails,
        BookingDetails details,
        Guest guest,
        Hotel hotel,
        List<Discount> appliedDiscounts) : base(Guid.NewGuid(), details, guest, hotel, appliedDiscounts)
    {
        PaymentMethod = paymentMethod;
        PriceDetails = priceDetails;
        CreatedAt = DateTime.UtcNow;
    }

    public static DraftBooking Create(
        DateTime from,
        DateTime to,
        short numberOfGuests,
        PaymentMethod paymentMethod,
        Guest guest,
        Hotel hotel,
        IEnumerable<Guid> guestDiscountIds)
    {
        var bookingDetails = BookingDetails.Create(from, to, numberOfGuests);

        hotel.EnsureBookable(bookingDetails, paymentMethod);
        guest.EnsureCanBook(bookingDetails);

        var discountsToBeApplied = guest.GetFilteredDiscountsByIds(guestDiscountIds);
        var initialPrice = hotel.CalculatePriceForNights(bookingDetails.Schedule.GetDurationInDays());
        var priceDetails = CalculatePriceDetails(initialPrice, guest, discountsToBeApplied);

        return new(paymentMethod, priceDetails, bookingDetails, guest, hotel, [.. discountsToBeApplied]);
    }


    public void Cancel(int guestId)
    {
        EnsureGuestIdMatches(guestId);
        Status = BookingStatus.Canceled;
    }

    private static PriceDetails CalculatePriceDetails(Money initialPrice, Guest guest, IEnumerable<Discount> appliedDiscounts)
    {
        var discountedPrice = guest.ApplyDiscounts(initialPrice, appliedDiscounts);
        return new(discountedPrice, initialPrice);
    }


}
