namespace TravelMore.Domain.Hotels.Exceptions;

public class HotelOverlapScheduleException : Exception
{
    public HotelOverlapScheduleException() : base("Schedule overlaps with other schdules")
    {
    }
}
