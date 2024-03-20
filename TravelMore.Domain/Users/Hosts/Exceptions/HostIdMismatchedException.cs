namespace TravelMore.Domain.Users.Hosts.Exceptions;

public class HostIdMismatchedException : Exception
{
    public HostIdMismatchedException() : base("The provided host id does not match the host ID of the hotel")
    {
    }
}
