using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Guests.Exceptions;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingDetails Details { get; private set; }
    public BookingPaymentDetails PaymentDetails { get; private set; }
    public int GuestId { get; private set; }
    public Guest Guest { get; private set; } = null!;
    public Guid BookedHotelId { get; private set; }
    public Hotel BookedHotel { get; private set; }
    public BookingStatus Status { get; private set; }


    /// <summary>
    /// Private consturctor needed to create database migrations
    /// </summary>
    private Booking() : base(Guid.NewGuid())
    {
        Details = null!;
        BookedHotel = null!;
        PaymentDetails = null!;
    }


    private Booking(
        BookingDetails details,
        Guest guest,
        Hotel bookedHotel) : base(Guid.NewGuid())
    {
        Details = details;
        Status = BookingStatus.Pending;
        Guest = guest;
        BookedHotel = bookedHotel;
        PaymentDetails = null!;
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
        var numberOfDays = schedule.GetDurationInDays();

        var bookingDetails = new BookingDetails(
            numberOfGuests,
            numberOfDays,
            schedule);

        hotel.EnsureBookable(bookingDetails, paymentMethod);
        guest.EnsureCanBook(bookingDetails);

        return new(bookingDetails, guest, hotel);
    }

    public void Accept(int hostId)
    {
        EnsureHotelHostIdMatches(hostId);
        EnsurePaymentCompleted();
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

    public void SetPaymentDetails(BookingPaymentDetails paymentDetails)
    {
        PaymentDetails = paymentDetails;
    }

    public bool IsPaymentMethodMatching(PaymentMethod paymentMethod) => paymentMethod == PaymentDetails!.PaymentMethod;

    public bool DoesOverLap(DateTime from, DateTime to) => Details.Schedule.From <= to && from <= Details.Schedule.To;

    public void EnsurePaymentCompleted()
    {
        //TODO: create custom exception 
        if (PaymentDetails.PaymentStatus != PaymentStatus.Completed)
        {
            throw new Exception();
        }
    }

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
