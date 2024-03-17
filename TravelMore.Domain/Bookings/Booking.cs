using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Requests;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingSchedule Schedule { get; private set; } = BookingSchedule.Create();
    public Money TotalPayment { get; private set; } = Money.Create(0).Value;
    public short NumberOfGuests { get; }
    public int GuestId { get; }
    public Guest Guest { get; } = null!;
    public Guid BookedHotelId { get; }
    public Hotel BookedHotel { get; } = null!;
    public BookingStatus Status { get; set; }

    private Booking() : base(Guid.NewGuid()) { }

    private Booking(BookingRequest request) : base(Guid.NewGuid())
    {
        Schedule = request.Schedule;
        TotalPayment = request.Payment;
        NumberOfGuests = request.NumberOfGuests;
        Guest = request.Guest;
        BookedHotel = request.Hotel;
        Status = BookingStatus.Pending;
    }

    public static Result<Booking> Create(
        DateTime from,
        DateTime to,
        short numberOfGuests,
        Guest guest,
        Hotel hotel)
    {
        var totalPaymentResult = new HotelPaymentCalculator(hotel, numberOfGuests)
            .Calculate();
        if (totalPaymentResult.IsFailure)
        {
            return Result.Failure<Booking>(totalPaymentResult.Error);
        }

        var scheduleResult = BookingSchedule.Create(from, to);
        if (scheduleResult.IsFailure)
        {
            return Result.Failure<Booking>(scheduleResult.Error);
        }

        var request = new BookingRequest(
            numberOfGuests,
            totalPaymentResult.Value,
            scheduleResult.Value,
            guest,
            hotel);

        var canBookHotelResult = guest.CanBook(request);
        if (canBookHotelResult.IsFailure)
        {
            return Result.Failure<Booking>(canBookHotelResult.Error);
        }

        return Result.Success<Booking>(new(request));
    }

    public Result SetSchedule(BookingSchedule schedule)
    {
        if (BookedHotel.AnyBookingsScheduleOverlaps(schedule))
        {
            return Result.Failure(DomainErrors.Hotel.OverlapSchedule);
        }

        Schedule = schedule;

        return Result.Success();
    }

    public Result Accept(int hostId)
    {
        if (BookedHotel.HostId != hostId)
        {
            return Result.Failure(DomainErrors.Booking.IncorrerctHostId);
        }

        Status = BookingStatus.Accepted;
        return Result.Success();
    }

    public Result Decline(int hostId)
    {
        if (BookedHotel.HostId != hostId)
        {
            return Result.Failure(DomainErrors.Booking.IncorrerctHostId);
        }

        Status = BookingStatus.Declined;
        return Result.Success();
    }

    public Result Cancel(int guestId)
    {
        if (Guest.Id != guestId)
        {
            return Result.Failure(DomainErrors.Booking.IncorrerctGuestId);
        }

        Status = BookingStatus.Canceled;
        return Result.Success();
    }

    public bool DoesOverLap(DateTime from, DateTime to) => Schedule.From <= to && from <= Schedule.To;

}
