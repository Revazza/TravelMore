using TravelMore.Domain.Common.Exceptions;

namespace TravelMore.Domain.Guests.Exceptions;

public class GuestOverlapBookingScheduleException : DomainException
{
    public GuestOverlapBookingScheduleException() : base("Schedule overlaps with other guest's bookings schedules")
    {
    }
}
