using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships.Coupons;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Coupons;

public class MembershipCouponConfigurations : IEntityTypeConfiguration<MembershipCoupon>
{
    public void Configure(EntityTypeBuilder<MembershipCoupon> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasOne(x => x.Target)
            .WithMany(x => x.Coupons)
            .HasForeignKey(x => x.TargetId);

        builder.ComplexProperty(x => x.Code);
        builder.ComplexProperty(x => x.DiscountAmount, MoneyConfigurations.DefaultPrecision);

    }
}
