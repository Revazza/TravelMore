
namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingDetails
{
    public short NumberOfGuests { get; init; }
    public short NumberOfDays { get; init; }
    public BookingSchedule Schedule { get; init; } = null!;

    private BookingDetails(
        short numberOfGuests,
        short numberOfDays,
        BookingSchedule schedule)
    {
        NumberOfDays = numberOfDays;
        NumberOfGuests = numberOfGuests;
        Schedule = schedule;
    }

    private BookingDetails() { }

    public static BookingDetails Create(
        DateTime from,
        DateTime to,
        short numberOfGuests)
    {
        var schedule = BookingSchedule.Create(from, to);
        var numberOfNights = schedule.GetDurationInDays();

        return new BookingDetails(
         numberOfGuests,
         numberOfNights,
         schedule);
    }

}