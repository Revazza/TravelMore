namespace TravelMore.Domain.Guests.Exceptions;

public class GuestNotFoundByEmailException : GuestNotFoundException
{
    public GuestNotFoundByEmailException(string email) : base($"Guest not found by email - {email}")
    {
    }
}
