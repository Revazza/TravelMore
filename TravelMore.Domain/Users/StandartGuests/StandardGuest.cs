using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.StandartGuests;

public class StandardGuest : Guest
{
    private StandardGuest() : base(0, string.Empty, string.Empty, string.Empty, Money.Default)
    {
    }

    public StandardGuest(int id, string email, string passwordHash, string salt, Money balance)
        : base(id, email, passwordHash, salt, balance)
    {
    }

}