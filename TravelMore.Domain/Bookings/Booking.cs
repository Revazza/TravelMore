using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Hotels;
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

    public static Result<Booking> Create(
        DateTime checkIn,
        DateTime checkOut,
        short numberOfGuests,
        Guest guest,
        Hotel hotel)
    {
        var schedule = new BookingSchedule(checkIn, checkOut);

        var isHotelAvailableResult = hotel.IsAvailable(schedule);
        if (isHotelAvailableResult.IsFailure)
        {
            return Result.Failure<Booking>(isHotelAvailableResult.Error);
        }

        var canBookHotelResult = guest.CanBookHotel(hotel, numberOfGuests);
        if (canBookHotelResult.IsFailure)
        {
            return Result.Failure<Booking>(canBookHotelResult.Error);
        }

        return Result.Success<Booking>(new(
            Guid.NewGuid(),
            schedule,
            hotel.CalculateTotalPayment(numberOfGuests),
            numberOfGuests,
            guest,
            hotel));
    }

    public void Accept() => Status = BookingStatus.Accepted;
    public void Decline() => Status = BookingStatus.Declined;

    public bool IsOverLap(DateTime from, DateTime to) => Schedule.From <= from && to <= Schedule.To;

}
