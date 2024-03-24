using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Hotels;

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

        builder.OwnsOne(x => x.PricePerDay, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

        SeedDatabase(builder);
    }

    public static void SeedDatabase(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(SeedHotel);

        builder.OwnsOne(x => x.PricePerDay).HasData(Price);
    }

    private static readonly Guid SeedHotelId = new("db37121d-c8fe-4f41-ab6e-34dded72f3b4");

    public readonly static dynamic SeedHotel = new
    {
        Id = new Guid("db37121d-c8fe-4f41-ab6e-34dded72f3b4"),
        Description = HostConfigurations.SeedHost.Email,
        Email = $"{HostConfigurations.SeedHost.Email}'s hotel",
        MaxNumberOfGuests = (short)10,
        HostId = HostConfigurations.SeedHost.Id
    };

    private static readonly dynamic Price = new
    {
        HotelId = SeedHotelId,
        Amount = 100m
    };

}
