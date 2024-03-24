using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel : Entity<Guid>
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string Description { get; } = string.Empty;
    public short MaxNumberOfGuests { get; set; }
    public Money PricePerNight { get; set; } = Money.Create(0);
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
        PricePerNight = pricePerNight;
        Host = host;
        HostId = Host.Id;
    }

    public void SetPricePerNight(decimal price)
    {
        PricePerNight = Money.Create(price);
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

    public void EnsureBookable(BookingSchedule schedule, short numberOfGuests)
    {
        EnsureNumberOfGuestsIsAllowed(numberOfGuests);
        EnsureNoBookingsScheduleOverlaps(schedule);
    }

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

    private void EnsureNoBookingsScheduleOverlaps(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            throw new HotelOverlapScheduleException();
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
