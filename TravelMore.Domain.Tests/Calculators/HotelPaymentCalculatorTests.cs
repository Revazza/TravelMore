using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Tests.Calculators;

public class HotelPaymentCalculatorTests
{
    private HotelPaymentCalculator _calculator;
    private Hotel _hotel;

    [SetUp]
    public void SetUp()
    {
        _hotel = new Hotel(Guid.NewGuid());
        _hotel.SetPricePerNight(50);
        short numberOfGuests = 5;

        _calculator = new HotelPaymentCalculator(_hotel, numberOfGuests);

    }

    [Test]
    public void Calculate_Should_ReturnsFailureResult_WhenNumberOfGuestsAreNegative()
    {
        var result = new HotelPaymentCalculator(_hotel, -1).Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Money.InvalidAmount));
        });
    }

    [Test]
    public void Calculate_Should_ReturnsSuccessResult_WhenValidParametersArePassed()
    {
        _hotel.SetPricePerNight(10);
        short numberOfGuests = 3;
        var expectedResult = _hotel.PricePerNight.Amount * numberOfGuests;
        var result = new HotelPaymentCalculator(_hotel, numberOfGuests).Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(result.Value.Amount, Is.EqualTo(expectedResult));
        });
    }

}
