using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;

namespace TravelMore.Domain.Users.Hosts;

public class Host : User
{

    private readonly List<Hotel> _hotels = [];
    public IReadOnlyCollection<Hotel> Hotels => _hotels;

    public List<BookingPaymentDetails> ReceivedPayments { get; set; } = [];

    private Host() : base(0, string.Empty, string.Empty, string.Empty)
    {
    }

    public Host(int id, string email, string passwordHash, string salt)
        : base(id, email, passwordHash, salt)
    {
    }

    public void AddHotel(Hotel newHotel) => _hotels.Add(newHotel);

    public void RemoveHotel(Hotel hotel) => _hotels.Remove(hotel);
}
