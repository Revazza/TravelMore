using System.Runtime.InteropServices.JavaScript;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Tests.TestsCommons;

public static partial class TestsCommon
{
    public static class Valid
    {
        public static Guest Guest => new(1, "Jack", Money.Create(1000).Value);
        public static Host Host => new(2, "Host@gmail.com");
        public static readonly DateTime FirstOfApril2023 = new(2024, 04, 01);
        public static readonly DateTime FifteenthOfApril2023 = new(2024, 04, 15);
        public static readonly BookingSchedule Schedule = BookingSchedule.Create(FirstOfApril2023, FifteenthOfApril2023).Value;
        public static readonly short ValidNumberOfGuests = 10;
        public static Hotel Hotel => new(
            id: new Guid("af9b9df3-a6a3-4f4b-bb1d-2fde6c4fcb40"),
            description: "Hotel Description",
            maxNumberOfGuests: ValidNumberOfGuests,
            pricePerNight: Money.Create(100).Value,
            host: Host);

    }


}
