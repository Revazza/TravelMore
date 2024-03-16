
using TravelMore.Domain.Common.Results;

namespace TravelMore.Domain.Errors;

public static partial class DomainErrors
{
    public static class Booking
    {
        public static readonly Error IncorrerctHostId = new("","Incorrect host id provided");
        public static readonly Error IncorrerctGuestId = new("","Incorrect guest id provided");
    }
}
