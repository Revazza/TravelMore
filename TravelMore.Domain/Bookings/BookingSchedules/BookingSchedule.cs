using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;

namespace TravelMore.Domain.Bookings.BookingSchedules;

public class BookingSchedule
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    private BookingSchedule(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public static Result<BookingSchedule> Create(DateTime from, DateTime to)
    {
        if (!IsBookingPeriodInOrder(from, to))
        {
            return DomainErrors.BookingSchedule.InvalidBookingPeriod;
        }

        return new BookingSchedule(from, to);
    }

    public static BookingSchedule Create() => new(DateTime.MinValue, DateTime.MinValue);

    private static bool IsBookingPeriodInOrder(DateTime from, DateTime to) => from <= to;

}
