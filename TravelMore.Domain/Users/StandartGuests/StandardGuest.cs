using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.PremiumGuests;

namespace TravelMore.Domain.Users.StandartGuests;

public class StandardGuest : Guest
{
    private StandardGuest() : base(0, string.Empty, string.Empty, string.Empty, Money.Default)
    {
    }

    public StandardGuest(string email, string passwordHash, string salt, Money balance)
        : base(0, email, passwordHash, salt, balance)
    {
    }

    public PremiumGuest CreatePremiumVersion()
    {
        EnsureBalanaceIsEnough(PremiumGuest.UpgradeCost);

        return new(Id, Email, PasswordHash, Salt, Balance);
    }

}