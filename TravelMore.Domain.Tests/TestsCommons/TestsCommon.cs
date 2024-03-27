using TravelMore.Domain.Bookings.ValueObjects;

namespace TravelMore.Domain.Tests.TestsCommons;

public static partial class TestsCommon
{
    public static readonly DateTime FirstOfApril2023 = new(2023, 04, 01);
    public static readonly DateTime FifteenthOfApril2023 = new(2023, 04, 15);
    public static BookingSchedule OverlapingSchedule => BookingSchedule.Create(
            FirstOfApril2023.AddDays(-5),
            FifteenthOfApril2023.AddDays(5));
    public static BookingSchedule NonOverlapingSchedule => BookingSchedule.Create(
            FifteenthOfApril2023.AddDays(1),
            FifteenthOfApril2023.AddDays(5));

}
