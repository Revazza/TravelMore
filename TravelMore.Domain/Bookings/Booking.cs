using System.Net.WebSockets;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Calculators;
using TravelMore.Domain.Common.Models;
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

    private Booking() : base(Guid.NewGuid())
    {
    }

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
        var schedule = BookingSchedule.Create(from, to);
        hotel.EnsureBookable(schedule, numberOfGuests);

        var totalPayment = HotelPaymentCalculator.Create(hotel, numberOfGuests).Calculate();
        guest.EnsureCanBook(schedule, totalPayment);

        return new(numberOfGuests, totalPayment, schedule, guest, hotel);
    }

    public void SetSchedule(BookingSchedule schedule)
    {
        BookedHotel.EnsureNoBookingsScheduleOverlaps(schedule);
        Guest.EnsureNoBookingsScheduleOverlaps(schedule);
        Schedule = schedule;
    }

    public void Accept(int hostId)
    {
        EnsureHostIdMatches(hostId);
        Status = BookingStatus.Accepted;
    }

    public void Decline(int hostId)
    {
        EnsureHostIdMatches(hostId);
        Status = BookingStatus.Declined;
    }

    public void Cancel(int guestId)
    {
        EnsureGuestIdMatches(guestId);
        Status = BookingStatus.Canceled;
    }

    public bool DoesOverLap(DateTime from, DateTime to) => Schedule.From <= to && from <= Schedule.To;

    private void EnsureGuestIdMatches(int guestId)
    {
        if (Guest.Id != guestId)
        {
            throw new GuestIdMismatchedException();
        }
    }

    private void EnsureHostIdMatches(int hostId)
    {
        if (BookedHotel.Host.Id != hostId)
        {
            throw new HostIdMismatchedException();
        }
    }

}
