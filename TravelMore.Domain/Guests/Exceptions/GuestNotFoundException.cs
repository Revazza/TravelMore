using TravelMore.Domain.Common.Exceptions;

namespace TravelMore.Domain.Guests.Exceptions;

public abstract class GuestNotFoundException : DomainException
{
    public GuestNotFoundException(string message) : base($"Guest not found {message}")
    {
    }
}
