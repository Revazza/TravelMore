
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Bookings.Exceptions;

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
    public void Create_Should_ReturnCreatedBookingSchedule_When_OrderedPeriodIsPassed(int days)
    {
        var from = _date;
        var to = _date.AddDays(days);

        var result = BookingSchedule.Create(from, to);

        Assert.Multiple(() =>
        {
            Assert.That(result.From, Is.EqualTo(from));
            Assert.That(result.To, Is.EqualTo(to));
        });

    }

    [Test]
    public void Create_Should_ThrowBookingScheduleInvalidPeriodException_When_UnorderedPeriodIsPassed()
    {
        var from = _date;
        var to = _date.AddDays(-1);

        Assert.Throws<BookingScheduleInvalidPeriodException>(() => BookingSchedule.Create(from, to));

    }

    [Test]
    public void Create_Should_ReturnBookingScheduleWithDefaultDatetimeValues_WhenNoParametersArePassed()
    {
        var result = BookingSchedule.Create();

        Assert.Multiple(() =>
        {
            Assert.That(result.From, Is.EqualTo(DateTime.MinValue));
            Assert.That(result.To, Is.EqualTo(DateTime.MinValue));
        });

    }

}
