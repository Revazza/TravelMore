using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;

namespace TravelMore.Domain.PaymentsDetails;

public abstract class BasePaymentDetails : Entity<Guid>
{
    public PaymentStatus PaymentStatus { get; protected set; }
    public PaymentMethod PaymentMethod { get; init; }
    public DateTime PaymentDate { get; protected set; }
    public DateTime CreatedAt { get; init; }
    public PriceDetails PriceDetails { get; init; }

    protected BasePaymentDetails() : base(Guid.NewGuid())
    {
        PriceDetails = null!;
    }

    protected BasePaymentDetails(PaymentMethod paymentMethod, PriceDetails priceDetails) : base(Guid.NewGuid())
    {
        PaymentMethod = paymentMethod;
        PriceDetails = priceDetails;
        CreatedAt = DateTime.UtcNow;
        PaymentStatus = PaymentStatus.Pending;
    }

}
