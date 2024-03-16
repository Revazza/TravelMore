using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Common.Requests;

public record BookingRequest(
    short NumberOfGuests,
    Money Payment,
    BookingSchedule Schedule,
    Guest Guest,
    Hotel Hotel);
