using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships.Discounts;

namespace TravelMore.Persistance.Configurations.Memberships.Discounts;

public class MembershipDiscountConfigurations : IEntityTypeConfiguration<MembershipDiscount>
{
    public void Configure(EntityTypeBuilder<MembershipDiscount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Target)
            .WithMany(x => x.Discounts)
            .HasForeignKey(x => x.TargetId);


    }
}
