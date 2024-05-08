namespace TravelMore.Application.Services.Payments.Dtos;

public record VisaPaymentDataDto(
    string CardNumber,
    string CardHolderName,
    int ExpirationMonth,
    int ExpirationYear,
    int CVV) : IPaymentMethodData;