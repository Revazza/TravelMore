using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users.Guests.Exceptions;

namespace TravelMore.Domain.Users.Guests;

public abstract class Guest : User
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string UserName { get; } = string.Empty;
    public Money Balance { get; private set; } = 0;

    protected Guest() : base(0)
    {
    }

    protected Guest(int id, string userName, Money balance) : base(id)
    {
        UserName = userName;
        Balance = balance;
    }

    protected Guest(int id, string userName) : base(id)
    {
        UserName = userName;
    }

    public virtual void EnsureCanBook(BookingDetails bookingDetails)
    {
        EnsureNoBookingsScheduleOverlaps(bookingDetails.Schedule);
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

    protected void EnsureBalanaceIsEnough(Money payment)
    {
        if (!IsBalanceEnough(payment))
        {
            throw new GuestInsufficientBalanceException();
        }
    }

    protected bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
