using TravelMore.Domain.Guests;

namespace TravelMore.Domain.Memberships;

public class PremiumMembership : Membership
{

    private PremiumMembership(Guid id) : base(id)
    {

    }

    private PremiumMembership(Guest guest) : base(guest)
    {

    }

    public static PremiumMembership Create(Guest guest)
    {

        return new(guest);
    }

}
