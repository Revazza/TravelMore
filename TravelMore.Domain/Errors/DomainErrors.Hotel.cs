using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Hotel
    {
        public static readonly Error OverlapSchedule = new(",", "Schedule overlaps with other schdules");
        public static readonly Error NotFound = new(",", "Hotel not found");
        public static readonly Error InvalidGuestNumber = new(",", "Guest number is less than or equal to zero, please add positive number");
    }
}
