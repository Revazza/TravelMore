using MediatR;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.PaymentsDetails.ValueObjects;

namespace TravelMore.Application.Discounts.Commands.ApplyDiscounts;

internal class ApplyDiscountsCommandHandler : IRequestHandler<ApplyDiscountsCommand, PriceDetails>
{

    public async Task<PriceDetails> Handle(ApplyDiscountsCommand request, CancellationToken cancellationToken)
    {
        var initialPrice = request.Price;
        var discountedPrice = GetDiscountedPrice(initialPrice, request.Discounts);

        return new PriceDetails(discountedPrice, initialPrice);
    }

    private Money GetDiscountedPrice(Money initialPrice, List<Discount> discounts)
        => discounts.Aggregate(initialPrice, (currentPrice, discount) => discount.Apply(currentPrice));


}
