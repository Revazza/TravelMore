using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel : Entity<Guid>
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string Description { get; } = string.Empty;
    public short MaxNumberOfGuests { get; set; }
    public Money PricePerNight { get; set; } = Money.Create(0).Value;
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

    public Result IsBookable(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            return DomainErrors.Hotel.OverlapSchedule;
        }
        return Result.Success();
    }

    public Result SetPricePerNight(decimal price)
    {
        var result = Money.Create(price);
        if (result.IsFailure)
        {
            return result;
        }

        PricePerNight = result.Value;
        return Result.Success();
    }

    public void AddBooking(Booking booking) => _bookings.Add(booking);

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

}
