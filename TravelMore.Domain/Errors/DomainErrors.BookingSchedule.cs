using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class BookingSchedule
    {
        public static readonly Error InvalidBookingPeriod = new("", "The booking period provided is invalid. Please ensure that the start date is before the end date");
    }
}
