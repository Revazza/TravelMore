using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Strategies;

namespace TravelMore.Domain.Tests.Discounts.Strategies;

public class FixedAmountDiscountTests
{

    [Fact]
    public void Should_Create_FixedAmountDiscount()
    {
        var exception = Record.Exception(() => new FixedAmountDiscount(Money.Default, Money.Default));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(100, 70, 30)]
    [InlineData(100, 100, 0)]
    [InlineData(100, 120, 0)]
    public void Apply_Should_Return_DiscountedPrice(decimal price, decimal discount, decimal discountedPrice)
    {
        var strategy = new FixedAmountDiscount(price, discount);

        var result = strategy.Apply();

        Assert.Equal(discountedPrice, result);

    }

}
