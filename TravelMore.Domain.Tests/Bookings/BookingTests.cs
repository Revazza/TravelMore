using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Tests.TestsCommons;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Tests.Bookings;

public class BookingTests
{
    private Hotel _hotel;
    private Guest _guest;
    private Host _host;
    private Booking _booking;

    [SetUp]
    public void SetUp()
    {
        _guest = TestsCommon.Valid.Guest;
        _guest.SetBalance(1000);
        _host = TestsCommon.Valid.Host;
        _hotel = new Hotel(
            id: new Guid("af9b9df3-a6a3-4f4b-bb1d-2fde6c4fcb40"),
            description: "Hotel Description",
            maxNumberOfGuests: TestsCommon.Valid.MaxNumberOfGuests,
            pricePerNight: Money.Create(100).Value,
            host: _host);

        _booking = Booking.Create(
            from: TestsCommon.FirstOfApril2023,
            to: TestsCommon.FifteenthOfApril2023,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            guest: _guest,
            hotel: _hotel).Value;

        _hotel.AddBooking(_booking);
        _host.AddHotel(_hotel);

    }


    #region SetSchedule

    [TestCase("2023-03-19", "2023-03-31")]
    [TestCase("2023-04-16", "2023-04-20")]
    public void SetSchedule_Should_ReturnSuccessResult_WhenNonOverlappingScheduleProvided(DateTime from, DateTime to)
    {
        var schedule = BookingSchedule.Create(from, to).Value;

        var result = _booking.SetSchedule(schedule);

        Assert.That(result.IsSuccess, Is.EqualTo(true));

    }

    [TestCase("2023-04-5", "2023-04-9")]
    [TestCase("2023-04-8", "2023-04-15")]
    [TestCase("2023-04-7", "2023-04-12")]
    [TestCase("2023-04-12", "2023-04-16")]
    public void SetSchedule_Should_ReturnFailureResult_WhenInvalidScheduleProvided(DateTime from, DateTime to)
    {
        var schedule = BookingSchedule.Create(from, to).Value;

        var result = _booking.SetSchedule(schedule);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Hotel.OverlapSchedule));
        });

    }

    #endregion

    #region Create

    [Test]
    public void Create_ShouldReturnFailureResult_WhenGuestCantBook_DueToScheduleOverlap()
    {
        var schedule = TestsCommon.OverlapingSchedule;
        var bookingResult = Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            guest: _guest,
            hotel: _hotel);

        Assert.Multiple(() =>
        {
            Assert.That(bookingResult.IsFailure, Is.EqualTo(true));
            Assert.That(bookingResult.Error, Is.EqualTo(DomainErrors.Hotel.OverlapSchedule));
        });

    }

    [Test]
    public void Create_ShouldReturnFailureResult_WhenGuestCantBook_DueToInsufficientBalance()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;
        _guest.SetBalance(0);
        var bookingResult = Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            guest: _guest,
            hotel: _hotel);

        Assert.Multiple(() =>
        {
            Assert.That(bookingResult.IsFailure, Is.EqualTo(true));
            Assert.That(bookingResult.Error, Is.EqualTo(DomainErrors.Guest.InsufficientBalance));
        });

    }

    [Test]
    public void Create_ShouldReturnSuccessResult_WhenGuestCanBook()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;

        var bookingResult = Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            guest: _guest,
            hotel: _hotel);

        Assert.Multiple(() =>
        {
            Assert.That(bookingResult.IsSuccess, Is.EqualTo(true));
            Assert.That(bookingResult.Error, Is.EqualTo(Error.None));
        });

    }

    [Test]
    public void Create_ShouldCreateBookingWithPendingStatus_WhenGuestCanBook()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;

        var result = Booking.Create(
            schedule.From,
            schedule.To,
            TestsCommon.Valid.NumberOfGuests,
            _guest,
            _hotel);

        Assert.That(result.Value.Status, Is.EqualTo(BookingStatus.Pending));

    }

    #endregion

    #region Accept
    [Test]
    public void Accept_ShouldReturnFailureResult_WhenHostIdDoesntMatchPassedHostId()
    {
        var incorrectHostId = -1;

        var result = _booking.Accept(incorrectHostId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Booking.IncorrerctHostId));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));
        });

    }

    [Test]
    public void Accept_ShouldReturnSuccessResultAndChangeStatus_WhenHostIdMatchesPassedHostId()
    {
        var result = _booking.Accept(_host.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Accepted));
        });

    }

    #endregion

    #region Cancel

    [Test]
    public void Cancel_ShouldReturnFailureResult_WhenGuestIdDoesntMatchPassedGuestId()
    {
        var incorrectGuestId = -1;

        var result = _booking.Cancel(incorrectGuestId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Booking.IncorrerctGuestId));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));
        });

    }

    [Test]
    public void Cancel_ShouldReturnSuccessResultAndChangeStatus_WhenGuestIdMatchesPassedGuestId()
    {
        var result = _booking.Cancel(_guest.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Canceled));
        });

    }

    #endregion

    #region Decline

    [Test]
    public void Decline_ShouldReturnSuccessResultAndChangeStatus_WhenHostIdMatchesPassedHostId()
    {
        var result = _booking.Decline(_host.Id);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(Error.None));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Declined));
        });

    }

    [Test]
    public void Decline_ShouldReturnFailureResult_WhenHostIdDoesntMatchPassedHostId()
    {
        var incorrectHostId = -1;
        var result = _booking.Decline(incorrectHostId);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(DomainErrors.Booking.IncorrerctHostId));
            Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));
        });

    }

    #endregion

    #region DoesOverlap

    [Test]
    public void DoesOverlap_ShouldReturnTrue_WhenScheduleOverlaps()
    {
        var schedule = TestsCommon.OverlapingSchedule;
        var result = _booking.DoesOverLap(schedule.From, schedule.To);

        Assert.That(result, Is.True);
    }

    [Test]
    public void DoesOverlap_ShouldReturnFalse_WhenScheduleDoesntOverlap()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;
        var result = _booking.DoesOverLap(schedule.From, schedule.To);

        Assert.That(result, Is.False);
    }

    #endregion

}
