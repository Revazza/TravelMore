using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Common.Requests;
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

    public void EnsureCanBook(Money payment)
    {
        if (!IsBalanceEnough(payment))
        {
            throw new GuestInsufficientBalanceException();
        }
    }

    public void SetBalance(decimal amount)
    {
        Balance = Money.Create(amount);
    }

    private bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
