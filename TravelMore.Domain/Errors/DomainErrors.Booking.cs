using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Booking
    {
        public static readonly Error HotelIsNotAvaiable = new Error("", "Hotel is not available");
    }
}
