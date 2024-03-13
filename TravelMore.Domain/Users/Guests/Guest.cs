using TravelMore.Domain.Bookings;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Shared.Models;

namespace TravelMore.Domain.Users.Guests;

public class Guest(int id) : User(id)
{
    private readonly List<Booking> _bookings = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public string UserName { get; } = string.Empty;
    public Money Balance { get; } = new(0);

    public bool CanBookHotel(Money totalPayment) => IsBalanceEnough(totalPayment);

    private bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
