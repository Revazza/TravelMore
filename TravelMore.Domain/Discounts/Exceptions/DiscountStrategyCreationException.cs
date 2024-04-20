namespace TravelMore.Domain.Discounts.Exceptions;

public class DiscountStrategyCreationException : Exception
{
    public DiscountStrategyCreationException() : base("Discount strategy doesn't exist for given DiscountType")
    {
    }
}
