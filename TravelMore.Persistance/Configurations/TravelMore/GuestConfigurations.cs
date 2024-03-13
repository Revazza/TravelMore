using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    }
}
