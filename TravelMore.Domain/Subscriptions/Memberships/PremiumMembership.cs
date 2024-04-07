using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Subscriptions.Memberships;

public class PremiumMembership : Membership
{
    public PremiumMembership()
    {
        
    }

    public static PremiumMembership Create(
        Money pricePerMonth,
        Money pricePerYear,
        DateTime startDate,
        DateTime endDate,
        List<SubscriptionTypeInfo> subscriptionTypes)
    {

        return new PremiumMembership(pricePerMonth,pricePerYear);
    }
}
