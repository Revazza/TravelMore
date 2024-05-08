using TravelMore.Application.Services.Payments;
using TravelMore.Application.Services.Payments.Dtos;

namespace TravelMore.Infrastructure.Services.Payments.Strategies;

public class VisaPayment : IPaymentService
{


    public Task<PaymentResultDto> PayAsync()
    {
        return null;
    }
}
