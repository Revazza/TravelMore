using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Hotels;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations;

public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Description)
            .HasMaxLength(HotelConstraints.MaxDescriptionLength);

        builder.HasOne(x => x.Host)
            .WithMany(x => x.Hotels)
            .HasForeignKey(x => x.HostId);

        builder.HasOne(x => x.Discount)
            .WithMany()
            .HasForeignKey(x => x.DiscountId);

        builder.ComplexProperty(x => x.PricePerDay, MoneyConfigurations.DefaultPrecision);

    }

}
