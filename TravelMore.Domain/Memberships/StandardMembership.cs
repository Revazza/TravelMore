
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts.Interfaces;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Memberships;

public class StandardMembership : Membership
{
    private StandardMembership(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<IDiscount> discounts)
            : base(id, pricePerMonth, pricePerYear, guest, discounts)
    {

    }

    public static StandardMembership Create(
        Guid id,
        Money pricePerMonth,
        Money pricePerYear,
        Guest guest,
        List<IDiscount> discounts)
    {

        return new(id, pricePerMonth, pricePerYear, guest, discounts);
    }

}
