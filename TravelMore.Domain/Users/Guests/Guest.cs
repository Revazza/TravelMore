using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.BookingSchedules;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests.Exceptions;

namespace TravelMore.Domain.Users.Guests;

public class Guest : User
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string UserName { get; } = string.Empty;
    public Money Balance { get; private set; } = Money.Create(0);

    private Guest() : base(0)
    {
    }

    public Guest(int id, string userName, Money balance) : base(id)
    {
        UserName = userName;
        Balance = balance;
    }

    public Guest(int id, string userName) : base(id)
    {
        UserName = userName;
    }

    public void EnsureCanBook(BookingSchedule schedule, Money payment)
    {
        EnsureBalanaceIsEnough(payment);
        EnsureNoBookingsScheduleOverlaps(schedule);
    }

    public void SetBalance(decimal amount)
    {
        Balance = Money.Create(amount);
    }

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

    public void EnsureNoBookingsScheduleOverlaps(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            throw new GuestOverlapBookingScheduleException();
        }
    }

    private void EnsureBalanaceIsEnough(Money payment)
    {
        if (!IsBalanceEnough(payment))
        {
            throw new GuestInsufficientBalanceException();
        }
    }

    private bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
