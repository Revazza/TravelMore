﻿using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Exceptions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Tests.Hotels;

public class HotelTests
{
    private Hotel _hotel;
    private Host _host;

    [SetUp]
    public void SetUp()
    {
        _host = new Host(1);
        _hotel = new Hotel(
            new Guid("cc6ba882-4a49-47c6-9084-c4b17738c254"),
            description: "Dummy Hotel",
            maxNumberOfGuests: 10,
            pricePerNight: Money.Create(10),
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
        var pricePerNight = Money.Create(10);
        var description = "description";
        short maxNumberOfGuests = 5;
        var result = new Hotel(hotelId, description, maxNumberOfGuests, pricePerNight, _host);

        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(hotelId));
            Assert.That(result.Description, Is.EqualTo(description));
            Assert.That(result.MaxNumberOfGuests, Is.EqualTo(maxNumberOfGuests));
            Assert.That(result.PricePerDay, Is.EqualTo(pricePerNight));
            Assert.That(result.Host, Is.EqualTo(_host));
            Assert.That(result.Bookings, Is.Empty);
        });

    }

    [Test]
    public void SetPricePerNight_Should_UpdatePricePerNight_When_NonNegativeParameterIsProvided()
    {
        var pricePerNight = 10;
        _hotel.SetPricePerDay(pricePerNight);

        Assert.That(_hotel.PricePerDay.Amount, Is.EqualTo(pricePerNight));
    }

    public void SetPricePerNight_Should_ReturnFailureResult_When_NegativeParameterIsProvided()
    {
        var invalidPricePerNight = -1;
        var initialPricePerNight = _hotel.PricePerDay;
        Assert.Throws<NegativeAmountException>(() => _hotel.SetPricePerDay(invalidPricePerNight));
        Assert.That(_hotel.PricePerDay, Is.EqualTo(initialPricePerNight));
    }

}
