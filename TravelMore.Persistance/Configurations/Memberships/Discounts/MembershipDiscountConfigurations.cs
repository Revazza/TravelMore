using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships.Discounts;

namespace TravelMore.Persistance.Configurations.Memberships.Discounts;

public class MembershipDiscountConfigurations : IEntityTypeConfiguration<MembershipDiscount>
{
    public void Configure(EntityTypeBuilder<MembershipDiscount> builder)
    {
        builder.HasOne(x => x.Membership)
            .WithMany(x => x.Discounts)
            .HasForeignKey(x => x.MembershipId)
            .OnDelete(DeleteBehavior.ClientSetNull);


    }
}
