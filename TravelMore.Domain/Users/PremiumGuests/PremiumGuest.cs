using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Users.PremiumGuests;

public class PremiumGuest : Guest
{
    public static readonly Money UpgradeCost = 100m;
    private PremiumGuest() : base(0, string.Empty, string.Empty, string.Empty, Money.Default)
    {
    }

    public PremiumGuest(string email, string passwordHash, string salt, Money balance)
        : base(0, email, passwordHash, salt, balance)
    {
    }

    public PremiumGuest(int id, string email, string passwordHash, string salt, Money balance)
        : base(id, email, passwordHash, salt, balance)
    {
    }

}
