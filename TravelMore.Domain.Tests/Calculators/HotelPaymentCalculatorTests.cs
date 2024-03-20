using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Tests.TestsCommons;

namespace TravelMore.Domain.Tests.Calculators;

public class HotelPaymentCalculatorTests
{
    private HotelPaymentCalculator _calculator;
    private Hotel _hotel;

    [SetUp]
    public void SetUp()
    {
        _hotel = new Hotel(new Guid("65834710-ec38-41df-89a0-c1e7290b47d1"));

        _hotel.SetPricePerNight(50);

        _calculator = HotelPaymentCalculator.Create(_hotel, TestsCommon.Valid.NumberOfGuests);

    }

    [Test]
    public void Calculate_Should_ThrowNegativeAmountException_WhenNumberOfGuestsAreNegative()
    {
        Assert.Throws<HotelInvalidGuestNumberException>(() => HotelPaymentCalculator.Create(_hotel, -1).Calculate());
    }

    [Test]
    public void Calculate_Should_ReturnsSuccessResult_WhenValidParametersArePassed()
    {
        _hotel.SetPricePerNight(10);
        short numberOfGuests = 3;
        var expectedResult = 30;
        var result = HotelPaymentCalculator.Create(_hotel, numberOfGuests).Calculate();

        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(expectedResult));
        });
    }

}
