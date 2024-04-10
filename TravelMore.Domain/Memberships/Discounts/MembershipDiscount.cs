using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;

namespace TravelMore.Domain.Memberships.Discounts;

public class MembershipDiscount : Discount<Membership, Guid>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public override bool IsExpired => EndDate <= DateTime.UtcNow;

    public MembershipDiscount(Guid id) : base(id)
    {
    }

    public override Money Apply(Money price)
    {
        throw new NotImplementedException();
    }
}
