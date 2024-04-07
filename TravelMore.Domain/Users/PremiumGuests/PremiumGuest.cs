using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.PremiumGuests;

public class PremiumGuest : Guest
{
    private PremiumGuest() : base(0, string.Empty, string.Empty, string.Empty, Money.Default, nameof(PremiumGuest))
    {
    }

    public PremiumGuest(int id, string email, string passwordHash, string salt, Money balance)
        : base(id, email, passwordHash, salt, balance, nameof(PremiumGuest))
    {
    }

}
