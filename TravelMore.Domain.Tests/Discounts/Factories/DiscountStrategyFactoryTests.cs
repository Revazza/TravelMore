using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Discounts.Exceptions;
using TravelMore.Domain.Discounts.Factories;
using TravelMore.Domain.Discounts.Strategies;

namespace TravelMore.Domain.Tests.Discounts.Factories;

public class DiscountStrategyFactoryTests
{

    [Theory]
    [InlineData(DiscountType.FixedPrice, typeof(FixedAmountDiscount))]
    [InlineData(DiscountType.Percentage, typeof(PercentageDiscount))]
    public void Create_Should_Return_CorrespondingStrategy(DiscountType discountType, Type expectedDiscountStrategyType)
    {
        var result = DiscountStrategyFactory.Create(Money.Default, Money.Default, discountType);

        Assert.IsAssignableFrom(expectedDiscountStrategyType, result);
    }

    [Fact]
    public void Create_Should_Throw_DiscountStrategyCreationException_When_PassedInvalidDiscountType()
    {
        var discountType = (DiscountType)int.MaxValue; // Invalid discount type

        Assert.Throws<DiscountStrategyCreationException>(() => DiscountStrategyFactory.Create(Money.Default, Money.Default, discountType));
    }

}
