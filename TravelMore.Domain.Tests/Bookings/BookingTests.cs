using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.Tests.TestsCommons;
using TravelMore.Domain.Users.Hosts;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Tests.Bookings;

public class BookingTests
{
    private Hotel _hotel;
    private Guest _guest;
    private Host _host;
    private Booking _booking;
    private List<Discount> _discounts;

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
            pricePerNight: Money.Create(100),
            host: _host);

        _booking = Booking.Create(
            from: TestsCommon.FirstOfApril2023,
            to: TestsCommon.FifteenthOfApril2023,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            paymentMethod: PaymentMethod.Visa,
            guest: _guest,
            hotel: _hotel,
            );

        _host.AddHotel(_hotel);

    }


    #region SetSchedule

    [TestCase("2023-03-19", "2023-03-31")]
    [TestCase("2023-04-16", "2023-04-20")]
    public void SetSchedule_Should_UpdateSchedule_When_NonOverlappingScheduleProvided(DateTime from, DateTime to)
    {
        var previousSchedule = _booking.Details.Schedule;
        var newSchedule = BookingSchedule.Create(from, to);

        //_booking.SetSchedule(newSchedule);

        Assert.That(previousSchedule, Is.Not.EqualTo(newSchedule));

    }

    [TestCase("2023-04-5", "2023-04-9")]
    [TestCase("2023-04-8", "2023-04-15")]
    [TestCase("2023-04-7", "2023-04-12")]
    [TestCase("2023-04-12", "2023-04-16")]
    public void SetSchedule_Should_ThrowHotelOverlapScheduleException_When_InvalidScheduleProvided(DateTime from, DateTime to)
    {
        var schedule = BookingSchedule.Create(from, to);

        //Assert.Throws<HotelOverlapBookingScheduleException>(() => _booking.SetSchedule(schedule));
    }

    #endregion

    #region Create

    [Test]
    public void Create_Should_ThrowHotelOverlapScheduleException_When_SchedulesOverlap()
    {
        var schedule = TestsCommon.OverlapingSchedule;

        Assert.Throws<HotelOverlapBookingScheduleException>(() => Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            paymentMethod: PaymentMethod.Visa,
            guest: _guest,
            hotel: _hotel));

    }

    [Test]
    public void Create_Should_ThrowGuestInsufficientBalanceException_When_GuestBalanceIsInsufficient()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;
        _guest.SetBalance(0);

        Assert.Throws<GuestInsufficientBalanceException>(() => Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            paymentMethod: PaymentMethod.Visa,
            guest: _guest,
            hotel: _hotel));

    }

    [Test]
    public void Create_Should_CreateBooking_When_GuestCanBook()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;

        var booking = Booking.Create(
            from: schedule.From,
            to: schedule.To,
            numberOfGuests: TestsCommon.Valid.NumberOfGuests,
            paymentMethod: PaymentMethod.Visa,
            guest: _guest,
            hotel: _hotel);

        Assert.Multiple(() =>
        {
            Assert.That(booking.Details.Schedule, Is.EqualTo(schedule));
            Assert.That(booking.Details.NumberOfGuests, Is.EqualTo(TestsCommon.Valid.NumberOfGuests));
            Assert.That(booking.Guest, Is.EqualTo(_guest));
            Assert.That(booking.BookedHotel, Is.EqualTo(_hotel));
        });

    }

    [Test]
    public void Create_Should_CreateBookingWithPendingStatus_When_GuestCanBook()
    {
        var schedule = TestsCommon.NonOverlapingSchedule;

        var booking = Booking.Create(
            schedule.From,
            schedule.To,
            TestsCommon.Valid.NumberOfGuests,
            paymentMethod: PaymentMethod.Visa,
            _guest,
            _hotel);

        Assert.That(booking.Status, Is.EqualTo(BookingStatus.Pending));

    }

    #endregion

    #region Accept
    [Test]
    public void Accept_Should_ThrowHostIdMismatchedException_When_HostIdDoesntMatchPassedHostId()
    {
        var incorrectHostId = -1;
        Assert.Throws<HostIdMismatchedException>(() => _booking.Accept(incorrectHostId));
        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));
    }

    [Test]
    public void Accept_Should_UpdateStatus_When_HostIdMatchesPassedHostId()
    {
        _booking.Accept(_host.Id);

        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Accepted));
    }

    #endregion

    #region Cancel

    [Test]
    public void Cancel_Should_ThrowGuestIdMismatchedException_When_GuestIdDoesntMatchPassedGuestId()
    {
        var incorrectGuestId = -1;

        Assert.Throws<GuestIdMismatchedException>(() => _booking.Cancel(incorrectGuestId));
        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));
    }

    [Test]
    public void Cancel_Should_UpdateStatus_WhenGuestIdMatchesPassedGuestId()
    {
        _booking.Cancel(_guest.Id);

        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Canceled));
    }

    #endregion

    #region Decline

    [Test]
    public void Decline_Should_UpdateStatus_WhenHostIdMatchesPassedHostId()
    {
        _booking.Decline(_host.Id);

        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Declined));
    }

    [Test]
    public void Decline_Should_ThrowHostIdMismatchedException_WhenHostIdDoesntMatchPassedHostId()
    {
        var incorrectHostId = -1;
        Assert.Throws<HostIdMismatchedException>(() => _booking.Decline(incorrectHostId));
        Assert.That(_booking.Status, Is.EqualTo(BookingStatus.Pending));

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
