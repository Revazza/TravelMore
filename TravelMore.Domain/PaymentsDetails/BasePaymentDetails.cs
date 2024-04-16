using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;

namespace TravelMore.Domain.PaymentsDetails;

public abstract class BasePaymentDetails : Entity<Guid>
{
    public PaymentStatus PaymentStatus { get; private set; }
    public PaymentMethod PaymentMethod { get; init; }
    public DateTime PaymentDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public PriceDetails PriceDetails { get; set; }

    protected BasePaymentDetails() : base(Guid.NewGuid())
    {
        PriceDetails = null!;
    }

    protected BasePaymentDetails(PaymentMethod paymentMethod) : base(Guid.NewGuid())
    {
        PaymentMethod = paymentMethod;
        PriceDetails = null!;
    }

}
