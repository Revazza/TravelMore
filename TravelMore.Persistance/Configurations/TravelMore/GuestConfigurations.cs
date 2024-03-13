using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Shared.Models;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Persistance.Configurations.TravelMore;

public class GuestConfigurations : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.Balance, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

        SeedDatabase(builder);
    }

    public static void SeedDatabase(EntityTypeBuilder<Guest> builder)
    {
        builder.HasData(SeedGuest);

        builder.OwnsOne(x => x.Balance).HasData(Balance);
    }

    public static readonly dynamic SeedGuest = new
    {
        Id = 1,
        UserName = "sandro",
    };

    private static readonly dynamic Balance = new
    {
        GuestId = 1,
        Amount = 100000m
    };

}
