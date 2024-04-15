using TravelMore.Domain.Discounts.Enums;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Memberships.Discounts;

namespace TravelMore.Domain.Memberships;

public class StandardMembership : Membership
{

    private StandardMembership(Guest guest) : base()
    {
        PricePerMonth = 10;
        PricePerYear = 100;
        Guest = guest;
    }

    public static StandardMembership Create(Guest guest)
    {

        return new StandardMembership(guest);
    }


    private List<MembershipDiscount> GetInitialDiscounts()
    {
        return
            [
                new MembershipDiscount(DiscountType.Percentage,DateTime.UtcNow,DateTime.UtcNow.AddDays(30),this)
            ];
    }

}
