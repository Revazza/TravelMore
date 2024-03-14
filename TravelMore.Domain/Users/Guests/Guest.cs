using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Users.Guests;

public class Guest : User
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string UserName { get; } = string.Empty;
    public Money Balance { get; } = new(0);

    public Guest(int id) : base(id)
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

    public bool CanBookHotel(Hotel hotel, short numberOfGuests) => IsBalanceEnough(hotel.CalculateTotalPayment(numberOfGuests));

    private bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
