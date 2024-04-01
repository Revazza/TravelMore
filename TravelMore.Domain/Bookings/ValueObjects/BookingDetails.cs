
namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingDetails
{
    public short NumberOfGuests { get; init; }
    public short NumberOfDays { get; init; }
    public BookingSchedule Schedule { get; init; } = null!;
    public BookingDetails(
        short numberOfGuests,
        short numberOfDays,
        BookingSchedule schedule)
    {
        NumberOfDays = numberOfDays;
        NumberOfGuests = numberOfGuests;
        Schedule = schedule;
    }

    private BookingDetails() { }

}