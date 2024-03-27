
namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingDetails(
        short NumberOfGuests,
        short NumberOfDays,
        BookingSchedule Schedule);