using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Shared.Models;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel : Entity<Guid>
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string Description { get; } = string.Empty;
    public short MaxNumberOfGuests { get; set; }
    public Money Price { get; set; } = new(0);
    public int OwnerId { get; }
    public Host Owner { get; } = null!;

    public Hotel(Guid id) : base(id)
    {

    }

    public Hotel(
        Guid id,
        string description,
        short maxNumberOfGuests,
        Money price,
        Host owner)
        : base(id)
    {
        Description = description;
        MaxNumberOfGuests = maxNumberOfGuests;
        Price = price;
        Owner = owner;
        OwnerId = Owner.Id;
    }

    public bool IsAvailable(BookingSchedule schedule) => AreAllBookingsOutsideSchedule(schedule);

    public Money CalculateTotalPayment(short numberOfGuests) => new(Price.Amount * numberOfGuests);

    private bool AreAllBookingsOutsideSchedule(BookingSchedule schedule) => _bookings.All(booking => !booking.IsOverLap(schedule.From, schedule.To));


}
