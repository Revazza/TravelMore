using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.StandartGuests;

public class StandardGuest : Guest
{
    private StandardGuest()
    {

    }

    public StandardGuest(int id, string userName, Money balance) : base(id, userName, balance) { }

}
