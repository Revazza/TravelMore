namespace TravelMore.Domain.Bookings.BookingSchedules;

public class BookingSchedule
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public BookingSchedule(DateTime from, DateTime to)
    {
        From = from;
        To = to;
    }

    public BookingSchedule()
    {
        From = DateTime.MinValue;
        To = DateTime.MinValue;
    }

}
