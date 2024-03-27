namespace TravelMore.Domain.Bookings.Exceptions;

public class BookingScheduleInvalidPeriodException : Exception
{
    public BookingScheduleInvalidPeriodException() : base("The booking period provided is invalid. Please ensure that the start date is before the end date")
    {
    }
}
