using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Shared.Models;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel(Guid id) : Entity<Guid>(id)
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string Description { get; } = string.Empty;
    public int MaxNumberOfGuests { get; set; }
    public Money Price { get; set; } = new(0);
    public int OwnerId { get; }
    public Host Owner { get; } = null!;

    public bool IsAvailable(BookingSchedule schedule) => AreAllBookingsOutsideSchedule(schedule);

    public Money CalculateTotalPayment(int numberOfGuests) => new(Price.Amount * numberOfGuests);

    private bool AreAllBookingsOutsideSchedule(BookingSchedule schedule) => _bookings.All(booking => !booking.IsOverLap(schedule.From, schedule.To));


}
