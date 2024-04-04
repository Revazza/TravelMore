using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.StandartGuests;

public class StandardGuest : Guest
{
    private StandardGuest()
    {

    }

    public StandardGuest(int id, string email, string passwordHash, string salt, Money balance) : base(id, email, passwordHash, salt, balance) { }

    public StandardGuest(string email, string passwordHash, string salt) : base(0, email, passwordHash, salt) { }


}
