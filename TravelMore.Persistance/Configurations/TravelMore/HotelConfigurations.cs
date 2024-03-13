using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Hotels;

namespace TravelMore.Persistance.Configurations.TravelMore;

public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Description)
            .HasMaxLength(HotelConstraints.MaxDescriptionLength);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Hotels)
            .HasForeignKey(x => x.OwnerId);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

    }
}
