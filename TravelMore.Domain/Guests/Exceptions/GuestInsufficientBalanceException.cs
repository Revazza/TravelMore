namespace TravelMore.Domain.Guests.Exceptions;

public class GuestInsufficientBalanceException : Exception
{
    public GuestInsufficientBalanceException() : base("Not enough balance in the account")
    {
    }
}
