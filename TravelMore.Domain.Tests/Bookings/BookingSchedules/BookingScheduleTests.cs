
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;

namespace TravelMore.Domain.Tests.Bookings.BookingSchedules;

public class BookingScheduleTests
{
    private DateTime _date;

    [SetUp]
    public void SetUp()
    {
        _date = new DateTime(2023, 04, 12);
    }

    [TestCase(0)]
    [TestCase(1)]
    public void Create_ShouldReturnSuccessResult_WhenOrderedPeriodIsPassed(int days)
    {
        var from = _date;
        var to = _date.AddDays(days);

        var result = BookingSchedule.Create(from, to);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(Error.None));
        });

    }

    [Test]
    public void Create_ShouldReturnFailureResult_WhenUnorderedPeriodIsPassed()
    {
        var from = _date;
        var to = _date.AddDays(-1);

        var result = BookingSchedule.Create(from, to);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.EqualTo(true));
            Assert.That(result.Error, Is.EqualTo(DomainErrors.BookingSchedule.InvalidBookingPeriod));
        });

    }

    [Test]
    public void Create_ShouldReturnBookingScheduleWithDefaultPropertyValues_WhenNoParametersArePassed()
    {
        var result = BookingSchedule.Create();

        Assert.Multiple(() =>
        {
            Assert.That(result.From, Is.EqualTo(DateTime.MinValue));
            Assert.That(result.To, Is.EqualTo(DateTime.MinValue));
        });

    }

}
