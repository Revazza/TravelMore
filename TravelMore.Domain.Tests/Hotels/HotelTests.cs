using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Tests.Hotels;

public class HotelTests
{
    private Hotel _hotel;
    private Host _host;
    private Booking _validBooking;

    [SetUp]
    public void SetUp()
    {
        _host = new Host(1);
        _hotel = new Hotel(
            new Guid("cc6ba882-4a49-47c6-9084-c4b17738c254"),
            description: "Dummy Hotel",
            maxNumberOfGuests: 10,
            pricePerNight: Money.Create(10).Value,
            _host);
    }

    [Test]
    public void Constructor_Should_CreateHotelWithGivenId()
    {
        var hotelId = new Guid("08008da6-69c8-426c-a69a-fbefd16b44e3");
        var result = new Hotel(hotelId);

        Assert.That(result.Id, Is.EqualTo(hotelId));

    }

    [Test]
    public void Constructor_Should_CreateHotelWithParameters()
    {
        var hotelId = new Guid("08008da6-69c8-426c-a69a-fbefd16b44e3");
        var pricePerNight = Money.Create(10).Value;
        var description = "description";
        short maxNumberOfGuests = 5;
        var result = new Hotel(hotelId, description, maxNumberOfGuests, pricePerNight, _host);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(hotelId));
            Assert.That(result.Description, Is.EqualTo(description));
            Assert.That(result.MaxNumberOfGuests, Is.EqualTo(maxNumberOfGuests));
            Assert.That(result.PricePerNight, Is.EqualTo(pricePerNight));
            Assert.That(result.Host, Is.EqualTo(_host));
            Assert.That(result.Bookings, Is.Empty);
        });

    }

    [Test]
    public void SetPricePerNight_ShouldReturnSuccessResult_WhenNonNegativeParameterIsProvided()
    {
        var pricePerNight = 10;
        var result = _hotel.SetPricePerNight(pricePerNight);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(_hotel.PricePerNight.Amount, Is.EqualTo(pricePerNight));
        });

    }

    public void SetPricePerNight_ShouldReturnFailureResult_WhenNegativeParameterIsProvided()
    {
        var invalidPricePerNight = -1;
        var initialPricePerNight = _hotel.PricePerNight;
        var result = _hotel.SetPricePerNight(invalidPricePerNight);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Money.InvalidAmount));
            Assert.That(_hotel.PricePerNight, Is.EqualTo(initialPricePerNight));
        });

    }

    public void AddBooking_Should_AddBookingToHotel()
    {

    }

}
