using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Domain.PaymentsDetails;

public abstract class BasePaymentDetails : Entity<Guid>
{
    public PaymentStatus PaymentStatus { get; private set; }
    public PaymentMethod PaymentMethod { get; init; }
    public DateTime PaymentDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public Money TotalPayment { get; init; } = 0;
    public Money Payment { get; init; } = 0;
    public Money Fee { get; init; } = 0;

    protected BasePaymentDetails(Guid id) : base(id)
    {
    }

    protected BasePaymentDetails(
        Money totalPayment,
        Money payment,
        Money fee,
        PaymentMethod paymentMethod) : base(Guid.NewGuid())
    {
        TotalPayment = totalPayment;
        Payment = payment;
        Fee = fee;
        PaymentMethod = paymentMethod;
    }

}
