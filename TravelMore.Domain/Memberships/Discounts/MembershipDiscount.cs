using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Discounts.Enums;

namespace TravelMore.Domain.Memberships.Discounts;

public class MembershipDiscount : BaseDiscount
{
    public DateTime StartDate { get; protected set; }
    public DateTime EndDate { get; protected set; }
    public Guid MembershipId { get; protected set; }
    public Membership Membership { get; protected set; } = null!;
    public override bool IsExpired => EndDate <= DateTime.UtcNow;

    public MembershipDiscount(
        DiscountType type,
        DateTime startDate,
        DateTime endDate,
        Membership membership) : base(type)
    {
        StartDate = startDate;
        EndDate = endDate;
        Membership = membership;
    }

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
