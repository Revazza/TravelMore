using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;

namespace TravelMore.Domain.Users.Hosts;

public class Host : User
{
    private readonly List<Hotel> _hotels = [];
    public IReadOnlyCollection<Hotel> Hotels => _hotels;
    public List<PaymentDetails> ReceivedPayments { get; set; } = [];

    public Host(int id) : base(id, string.Empty, string.Empty, string.Empty)
    {

    }

    public Host(int id, string email, string passwordHash, string salt) : base(id, email, passwordHash, salt)
    {
    }

    public void AddHotel(Hotel newHotel) => _hotels.Add(newHotel);
    public void RemoveHotel(Hotel newHotel) => _hotels.Remove(newHotel);

}
