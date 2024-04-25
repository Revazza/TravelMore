using MediatR;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.PaymentsDetails.ValueObjects;

namespace TravelMore.Application.Discounts.Commands.ApplyDiscounts;

public record ApplyDiscountsCommand(Money Price, List<Discount> Discounts) : IRequest<PriceDetails>;
