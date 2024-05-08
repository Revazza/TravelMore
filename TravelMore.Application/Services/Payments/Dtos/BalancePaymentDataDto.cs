using TravelMore.Domain.Common.Models;

namespace TravelMore.Application.Services.Payments.Dtos;

public record BalancePaymentDataDto(Money Amount) : IPaymentMethodData;
