using TravelMore.Application.Services.Payments.Dtos;

namespace TravelMore.Application.Services.Payments;

public interface IPaymentService
{
    Task<PaymentResultDto> PayAsync();
}
