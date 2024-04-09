using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships;

namespace TravelMore.Persistance.Configurations.Memberships;

public class MembershipConfigurations : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasOne(x => x.Guest)
            .WithOne(x => x.Membership)
            .HasForeignKey<Membership>(m => m.GuestId);

        builder.ComplexProperty(x => x.PricePerMonth, amount =>
        {
            amount.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

        builder.ComplexProperty(x => x.PricePerYear, amount =>
        {
            amount.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

    }
}
