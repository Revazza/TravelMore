using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel : Entity<Guid>
{
    private readonly List<Booking> _bookings = [];
    private readonly List<PaymentMethod> _acceptedPaymentMethods = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public IReadOnlyCollection<PaymentMethod> AcceptedPaymentMethods => _acceptedPaymentMethods;
    public string Description { get; } = string.Empty;
    public short MaxNumberOfGuests { get; set; }
    public Money PricePerDay { get; set; } = Money.Create(0);
    public int HostId { get; }
    public Host Host { get; } = null!;

    public Hotel(Guid id) : base(id)
    {

    }

    public Hotel(
        Guid id,
        string description,
        short maxNumberOfGuests,
        Money pricePerNight,
        Host host)
        : base(id)
    {
        Description = description;
        MaxNumberOfGuests = maxNumberOfGuests;
        PricePerDay = pricePerNight;
        Host = host;
        HostId = Host.Id;
    }

    public void SetPricePerDay(decimal price)
    {
        PricePerDay = Money.Create(price);
    }

    public void AddBooking(Booking booking)
    {
        _bookings.Add(booking);
    }

    public void SetMaxNumberOfGuests(short numberOfGuests)
    {
        if (numberOfGuests.IsLessThanOrEqualToZero())
        {
            throw new HotelInvalidGuestNumberException();
        }

        MaxNumberOfGuests = numberOfGuests;
    }

    public void EnsureBookable(BookingDetails bookingDetails, PaymentDetails paymentDetails)
    {
        EnsureNoBookingsScheduleOverlaps(bookingDetails.Schedule);
        EnsureNumberOfGuestsIsAllowed(bookingDetails.NumberOfGuests);
        EnsureAcceptsPaymentMethod(paymentDetails.PaymentMethod);
    }

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

    private void EnsureNoBookingsScheduleOverlaps(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            throw new HotelOverlapBookingScheduleException();
        }
    }

    private void EnsureAcceptsPaymentMethod(PaymentMethod paymentMethod)
    {
        //TODO: create custom exception
        if (!AcceptedPaymentMethods.Any(method => method == paymentMethod))
        {
            throw new Exception("Hotel doesn't accept given payment method");
        }
    }

    private void EnsureNumberOfGuestsIsAllowed(short numberOfGuests)
    {
        if (!IsNumberOfGuestsAllowed(numberOfGuests))
        {
            throw new Exception();// TODO: create custom exception
        }
    }

    private bool IsNumberOfGuestsAllowed(short numberOfguests) => numberOfguests <= MaxNumberOfGuests;

}
