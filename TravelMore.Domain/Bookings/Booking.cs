using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Requests;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Guests.Exceptions;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingSchedule Schedule { get; private set; } = BookingSchedule.Create();
    public Money TotalPayment { get; private set; } = Money.Create(0);
    public short NumberOfGuests { get; }
    public int GuestId { get; }
    public Guest Guest { get; } = null!;
    public Guid BookedHotelId { get; }
    public Hotel BookedHotel { get; } = null!;
    public BookingStatus Status { get; set; }

    private Booking(
        short numberOfGuests,
        Money totalPayment,
        BookingSchedule schedule,
        Guest guest,
        Hotel bookedHotel) : base(Guid.NewGuid())
    {
        NumberOfGuests = numberOfGuests;
        TotalPayment = totalPayment;
        Schedule = schedule;
        Guest = guest;
        BookedHotel = bookedHotel;
        Status = BookingStatus.Pending;
    }

    public static Booking Create(
        DateTime from,
        DateTime to,
        short numberOfGuests,
        Guest guest,
        Hotel hotel)
    {
        var totalPayment = new HotelPaymentCalculator(hotel, numberOfGuests).Calculate();
        var schedule = BookingSchedule.Create(from, to);

        guest.EnsureCanBook(totalPayment);
        hotel.EnsureBookable(schedule);

        return new(numberOfGuests, totalPayment, schedule, guest, hotel);
    }

    public void SetSchedule(BookingSchedule schedule)
    {
        if (BookedHotel.AnyBookingsScheduleOverlaps(schedule))
        {
            throw new HotelOverlapScheduleException();
        }

        Schedule = schedule;
    }

    public void Accept(int hostId)
    {
        if (BookedHotel.HostId != hostId)
        {
            throw new HostIdMismatchedException();
        }

        Status = BookingStatus.Accepted;
    }

    public void Decline(int hostId)
    {
        if (BookedHotel.HostId != hostId)
        {
            throw new HostIdMismatchedException();
        }

        Status = BookingStatus.Declined;
    }

    public void Cancel(int guestId)
    {
        if (Guest.Id != guestId)
        {
            throw new GuestIdMismatchedException();
        }

        Status = BookingStatus.Canceled;
    }

    public bool DoesOverLap(DateTime from, DateTime to) => Schedule.From <= to && from <= Schedule.To;

}
