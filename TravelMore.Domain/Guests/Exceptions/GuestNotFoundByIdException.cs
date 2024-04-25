namespace TravelMore.Domain.Guests.Exceptions;

public class GuestNotFoundByIdException : GuestNotFoundException
{
    public GuestNotFoundByIdException(int id) : base($"Guest not found by id - {id}")
    {
    }
}
