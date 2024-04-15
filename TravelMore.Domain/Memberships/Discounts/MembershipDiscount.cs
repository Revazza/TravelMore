using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Memberships.Discounts;

public class MembershipDiscount : BaseDiscount
{
    public DateTime StartDate { get; protected set; }
    public DateTime EndDate { get; protected set; }
    public Guid MembershipId { get; protected set; }
    public Membership Membership { get; protected set; } = null!;
    public override bool IsExpired => EndDate <= DateTime.UtcNow;

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
