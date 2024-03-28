using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Guests.Exceptions;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingDetails Details { get; private set; } = null!;
    public PaymentDetails PaymentDetails { get; private set; }
    public int GuestId { get; private set; }
    public Guest Guest { get; private set; } = null!;
    public Guid BookedHotelId { get; private set; }
    public Hotel BookedHotel { get; private set; } = null!;
    public BookingStatus Status { get; private set; }

    private Booking() : base(Guid.NewGuid())
    {
    }


    private Booking(
        PaymentDetails paymentDetails,
        BookingDetails details,
        Guest guest,
        Hotel bookedHotel) : base(Guid.NewGuid())
    {
        PaymentDetails = paymentDetails;
        Details = details;
        Status = BookingStatus.Pending;
        Guest = guest;
        BookedHotel = bookedHotel;
        PaymentDetails = null;
    }

    public static Booking Create(
       DateTime from,
       DateTime to,
       short numberOfGuests,
       PaymentMethod paymentMethod,
       Guest guest,
       Hotel hotel)
    {
        var schedule = BookingSchedule.Create(from, to);
        var numberOfDays = schedule.GetBookingDurationInDays();

        var bookingDetails = new BookingDetails(
            NumberOfGuests: numberOfGuests,
            NumberOfDays: numberOfDays,
            Schedule: schedule);

        var paymentDetails = new PaymentDetails(paymentMethod);

        hotel.EnsureBookable(bookingDetails, paymentMethod);
        guest.EnsureCanBook(bookingDetails);

        return new(paymentDetails, bookingDetails, guest, hotel);
    }

    public void Accept(int hostId)
    {
        EnsureHotelHostIdMatches(hostId);
        Status = BookingStatus.Accepted;
    }

    public void Decline(int hostId)
    {
        EnsureHotelHostIdMatches(hostId);
        EnsureNotAccepted();
        Status = BookingStatus.Declined;
    }

    public void Cancel(int guestId)
    {
        EnsureGuestIdMatches(guestId);
        Status = BookingStatus.Canceled;
    }

    public void SetPaymentDetails(PaymentDetails paymentDetails)
    {
        PaymentDetails = paymentDetails;
    }

    public void ChangePaymentMethod(PaymentMethod paymentMethod)
    {
        BookedHotel.EnsureAcceptedPaymentMethod(paymentMethod);
        PaymentDetails.PaymentMethod = paymentMethod;
    }

    public bool IsPaymentMethodMatching(PaymentMethod paymentMethod) => paymentMethod == PaymentDetails.PaymentMethod;

    public bool DoesOverLap(DateTime from, DateTime to) => Details.Schedule.From <= to && from <= Details.Schedule.To;

    private void EnsureNotAccepted()
    {
        //TODO: create custom exception 
        if (Status == BookingStatus.Accepted)
        {
            throw new Exception();
        }
    }

    private void EnsureGuestIdMatches(int guestId)
    {
        if (Guest.Id != guestId)
        {
            throw new GuestIdMismatchedException();
        }
    }

    private void EnsureHotelHostIdMatches(int hostId)
    {
        if (BookedHotel.Host.Id != hostId)
        {
            throw new HostIdMismatchedException();
        }
    }

}
