namespace TravelMore.Domain.Common.Exceptions;

public class NegativeAmountException : DomainException
{
    public NegativeAmountException() : base("Amount must be non-negative")
    {
    }
}
