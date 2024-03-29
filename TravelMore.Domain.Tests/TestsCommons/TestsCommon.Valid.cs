﻿using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Tests.TestsCommons;

public static partial class TestsCommon
{
    public static class Valid
    {
        public static Guest Guest => new(1, "Jack", Money.Create(1000));
        public static Host Host => new(2, "Host@gmail.com");
        public static readonly BookingSchedule Schedule = BookingSchedule.Create(FirstOfApril2023, FifteenthOfApril2023);
        public static readonly short NumberOfGuests = 5;
        public static readonly short MaxNumberOfGuests = 10;

    }


}
