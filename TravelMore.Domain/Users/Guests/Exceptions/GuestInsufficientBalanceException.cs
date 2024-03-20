namespace TravelMore.Domain.Users.Guests.Exceptions;

public class GuestInsufficientBalanceException : Exception
{
    public GuestInsufficientBalanceException() : base("Not enough balance in the account")
    {
    }
}
