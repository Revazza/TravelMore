using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.PaymentsDetails.ValueObjects;

public record PriceDetails
{
    public PriceDetails() { }
    public PriceDetails(Money actualPayment, Money initialPrice, Money discountedPrice)
    {
        ActualPayment = actualPayment;
        InitialPrice = initialPrice;
        DiscountedPrice = discountedPrice;
    }

    public Money ActualPayment { get; init; } = 0;
    public Money InitialPrice { get; init; } = 0;
    public Money DiscountedPrice { get; init; } = 0;
}