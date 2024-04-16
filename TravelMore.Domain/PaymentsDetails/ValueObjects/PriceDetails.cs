using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.PaymentsDetails.ValueObjects;

public record PriceDetails(Money ActualPayment, Money InitialPrice, Money DiscountedPrice);
