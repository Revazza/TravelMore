namespace TravelMore.Domain.Common.Exceptions;

public class NegativeAmountException : Exception
{
    public NegativeAmountException() : base("Amount must be non-negative")
    {
    }
}
