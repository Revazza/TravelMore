
using TravelMore.Domain.Bookings.Exceptions;

namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingSchedule
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    private BookingSchedule(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public static BookingSchedule Create(DateTime from, DateTime to)
    {
        EnsureBookingPeriodIsInOrder(from, to);
        return new(from, to);
    }

    public static BookingSchedule Create() => new(DateTime.MinValue, DateTime.MinValue);

    public short GetDurationInDays() => (short)(To - From).TotalDays;

    private static void EnsureBookingPeriodIsInOrder(DateTime from, DateTime to)
    {
        if (!IsBookingPeriodInOrder(from, to))
        {
            throw new BookingScheduleInvalidPeriodException();
        }
    }

    private static bool IsBookingPeriodInOrder(DateTime from, DateTime to) => from <= to;

}
