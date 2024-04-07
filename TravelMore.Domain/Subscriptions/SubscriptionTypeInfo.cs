using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Subscriptions.Enums;

namespace TravelMore.Domain.Subscriptions.ValueObjects;

public class SubscriptionTypeInfo : Entity<Guid>
{
    public SubscriptionType Type { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public List<Subscription> Subscriptions { get; set; } = [];

    private SubscriptionTypeInfo() : base(Guid.NewGuid())
    {
    }

    public SubscriptionTypeInfo(SubscriptionType type, string description) : base(Guid.NewGuid())
    {
        Type = type;
        Description = description;
    }

}
