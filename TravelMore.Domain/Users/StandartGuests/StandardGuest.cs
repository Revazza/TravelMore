using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.StandartGuests;

public class StandardGuest : Guest
{
    private StandardGuest()
    {

    }

    public StandardGuest(int id, string userName, string passwordHash, string salt, Money balance) : base(id, userName, passwordHash, salt, balance) { }

    public StandardGuest(string userName, string passwordHash, string salt) : base(0, userName, passwordHash, salt) { }


}
