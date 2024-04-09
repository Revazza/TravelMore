using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Coupons.MembershipCoupons;

namespace TravelMore.Persistance.Configurations.Coupons;

public class MembershipCouponConfigurations : IEntityTypeConfiguration<MembershipCoupon>
{
    public void Configure(EntityTypeBuilder<MembershipCoupon> builder)
    {

        builder.HasOne(x => x.Target)
            .WithMany(x => x.Coupons)
            .HasForeignKey(x => x.TargetId);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.OwnsOne(x => x.Code);
        builder.OwnsOne(x => x.DiscountAmount, amount =>
        {
            amount.Property(x => x.Amount).HasPrecision(18, 10);
        });

    }
}
