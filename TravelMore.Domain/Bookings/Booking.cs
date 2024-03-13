using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Shared.Models;
using TravelMore.Domain.Shared.Results;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingSchedule Schedule { get; } = new();
    public Money TotalPayment { get; } = new(0);
    public short NumberOfGuests { get; }
    public int GuestId { get; }
    public Guest Guest { get; } = null!;
    public Guid BookedHotelId { get; }
    public Hotel BookedHotel { get; } = null!;
    public BookingStatus Status { get; set; }

    private Booking() : base(Guid.NewGuid()) { }

    private Booking(Guid id, BookingSchedule schedule, Money totalPayment, short numberOfGuests, Guest guest, Hotel hotel) : base(id)
    {
        Schedule = schedule;
        NumberOfGuests = numberOfGuests;
        Guest = guest;
        BookedHotel = hotel;
        TotalPayment = totalPayment;
        Status = BookingStatus.Pending;
    }

    public static Result<Booking> CreateBooking(
        DateTime checkIn,
        DateTime checkOut,
        short numberOfGuests,
        Guest guest,
        Hotel hotel)
    {
        var schedule = new BookingSchedule(checkIn, checkOut);

        if (!hotel.IsAvailable(schedule))
        {
            return Result<Booking>.Failure(Error.None);
        }

        var totalPayment = hotel.CalculateTotalPayment(numberOfGuests);

        if (!guest.CanBookHotel(totalPayment))
        {
            return Result<Booking>.Failure(Error.None);
        }

        return Result<Booking>.Success(new(
            Guid.NewGuid(),
            schedule,
            totalPayment,
            numberOfGuests,
            guest,
            hotel));
    }

    public bool IsOverLap(DateTime from, DateTime to) => Schedule.From <= from && to <= Schedule.To;

}
