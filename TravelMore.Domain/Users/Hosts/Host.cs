using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Users.Hosts;

public class Host : User
{
    private readonly List<Hotel> _hotels = [];
    public IReadOnlyCollection<Hotel> Hotels => _hotels;
    public string Email { get; set; } = string.Empty;

    public Host(int id) : base(id)
    {

    }

    public Host(int id, string email) : base(id)
    {
        Email = email;
    }

    public void AddHotel(Hotel newHotel) => _hotels.Add(newHotel);
    public void RemoveHotel(Hotel newHotel) => _hotels.Remove(newHotel);

}
