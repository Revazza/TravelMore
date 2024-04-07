using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Subscriptions.ValueObjects;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.Subscriptions.Memberships;

public abstract class Membership : Subscription
{
    public List<Guest> Members { get; set; } = [];

    protected Membership() : base(Guid.NewGuid())
    {

    }

    protected Membership(
        Money pricePerMonth,
        Money pricePerYear,
        DateTime startDate,
        DateTime endDate,
        List<SubscriptionTypeInfo> subscriptionTypes)
            : base(pricePerMonth, pricePerYear, startDate, endDate, subscriptionTypes)
    {

    }

}
