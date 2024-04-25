using TravelMore.Domain.Common.Exceptions;

namespace TravelMore.Domain.Guests.Exceptions;

public class GuestIdMismatchedException : DomainException
{
    public GuestIdMismatchedException() : base("The provided guest id does not match the guest id of the hotel")
    {
    }
}
