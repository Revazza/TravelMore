using TravelMore.Domain.Common.Exceptions;

namespace TravelMore.Domain.Guests.Exceptions;

public class GuestInsufficientBalanceException : DomainException
{
    public GuestInsufficientBalanceException() : base("Not enough balance in the account")
    {
    }
}
