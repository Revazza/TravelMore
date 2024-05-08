using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Application.Services.Payments;

public interface IPaymentServiceProvider
{
    IPaymentService Provide(IPaymentMethodData paymentData, PaymentMethod paymentMethod);
}
