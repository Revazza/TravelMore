using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Subscriptions.Enums;
using TravelMore.Domain.Subscriptions.ValueObjects;

namespace TravelMore.Domain.Subscriptions;

public abstract class Subscription : Entity<Guid>
{
    protected readonly List<SubscriptionTypeInfo> _subscriptionTypeInfos = [];
    public IReadOnlyCollection<SubscriptionTypeInfo> SubscriptionTypeInfos => _subscriptionTypeInfos;
    public string Description { get; private set; } = string.Empty;
    public Money PricePerMonth { get; private set; } = 0;
    public Money PricePerYear { get; private set; } = 0;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsExpired => EndDate < DateTime.UtcNow;
    public SubscriptionDuration Duration { get; set; }

    protected Subscription(Guid id) : base(id)
    {

    }

    protected Subscription(
        Money pricePerMonth,
        Money pricePerYear,
        DateTime startDate,
        DateTime endDate,
        List<SubscriptionTypeInfo> subscriptionTypeInfos
        ) : base(Guid.NewGuid())
    {
        Description = GetSubscriptionTypeInfosDescription(subscriptionTypeInfos);
        PricePerMonth = pricePerMonth;
        PricePerYear = pricePerYear;
        StartDate = startDate;
        EndDate = endDate;
    }

    private static string GetSubscriptionTypeInfosDescription(List<SubscriptionTypeInfo> subscriptionTypeInfos)
        => string.Join(",\n", subscriptionTypeInfos.Select(x => x.Description));

}

