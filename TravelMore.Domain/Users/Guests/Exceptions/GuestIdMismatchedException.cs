namespace TravelMore.Domain.Users.Guests.Exceptions;

public class GuestIdMismatchedException : Exception
{
    public GuestIdMismatchedException() : base("The provided guest id does not match the guest id of the hotel")
    {
    }
}
