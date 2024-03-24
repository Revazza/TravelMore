namespace TravelMore.Domain.Hotels.Exceptions;

public class HotelOverlapBookingScheduleException : Exception
{
    public HotelOverlapBookingScheduleException() : base("Schedule overlaps with other hotel's booking schedules")
    {
    }
}
