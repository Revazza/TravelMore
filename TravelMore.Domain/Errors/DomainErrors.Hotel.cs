using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Hotel
    {
        public static readonly Error OverlapSchedule = new(",", "Schedule overlaps with other schdules");
    }
}
