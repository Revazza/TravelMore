using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Memberships;

public class MembershipConfigurations : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasOne(x => x.Guest)
            .WithOne(x => x.Membership)
            .HasForeignKey<Membership>(m => m.GuestId);

        builder.ComplexProperty(x => x.PricePerMonth, MoneyConfigurations.DefaultPrecision);

        builder.ComplexProperty(x => x.PricePerYear, MoneyConfigurations.DefaultPrecision);

    }
}
