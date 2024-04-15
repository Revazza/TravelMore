using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Guests;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Guests;

public class GuestConfigurations : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.ComplexProperty(x => x.Balance, MoneyConfigurations.DefaultPrecision);
        builder.HasMany(x => x.Discounts)
            .WithOne();
    }
}
