using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Memberships;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users.Guests.Exceptions;

namespace TravelMore.Domain.Users.Guests;

public class Guest : User
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public Money Balance { get; private set; } = 0;
    public List<BookingPaymentDetails> Payments { get; set; } = [];
    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; } = null!;

    private Guest() : base(0, string.Empty, string.Empty, string.Empty)
    {

    }

    protected Guest(int id, string email, string passwordHash, string salt, Money balance)
        : base(id, email, passwordHash, salt)
    {
        Balance = balance;
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
