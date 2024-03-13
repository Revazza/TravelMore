using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Users.Hosts;

public class Host(int id) : User(id)
{
    private readonly List<Hotel> _hotels = [];
    public IReadOnlyCollection<Hotel> Hotels => _hotels;

    public void AddHotel(Hotel newHotel) => _hotels.Add(newHotel);
    public void RemoveHotel(Hotel newHotel) => _hotels.Remove(newHotel);

}
