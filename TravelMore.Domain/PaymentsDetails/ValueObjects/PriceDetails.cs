using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.PaymentsDetails.ValueObjects;

public record PriceDetails
{
    public PriceDetails() { }
    public PriceDetails(Money discountedPrice, Money initialPrice)
    {
        DiscountedPrice = discountedPrice;
        InitialPrice = initialPrice;
        DiscountedAmount = initialPrice - discountedPrice;
    }

    public Money DiscountedPrice { get; init; } = 0;
    public Money InitialPrice { get; init; } = 0;
    public Money DiscountedAmount { get; init; } = 0;
}