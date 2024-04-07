using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Domain.PaymentsDetails;

public class PremiumPacketPaymentDetails : BasePaymentDetails
{
    private PremiumPacketPaymentDetails(Guid id) : base(id)
    {
    }

    private PremiumPacketPaymentDetails(Money totalPayment, Money payment, Money fee) : base(totalPayment, payment, fee, PaymentMethod.FromBalance)
    {
    }

    public static PremiumPacketPaymentDetails Create(Money totalPayment, Money payment, Money fee)
    {
        return new(totalPayment, payment, fee);
    }

}
