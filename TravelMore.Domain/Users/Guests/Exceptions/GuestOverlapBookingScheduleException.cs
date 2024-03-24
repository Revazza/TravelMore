namespace TravelMore.Domain.Users.Guests.Exceptions;

public class GuestOverlapBookingScheduleException : Exception
{
    public GuestOverlapBookingScheduleException() : base("Schedule overlaps with other guest's bookings schedules")
    {
    }
}
