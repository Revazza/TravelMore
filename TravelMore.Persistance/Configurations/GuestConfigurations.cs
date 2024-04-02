using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Persistance.Configurations;

public class GuestConfigurations : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        builder.Property(x => x.UserName).IsRequired();
        builder.ComplexProperty(x => x.Balance, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });
    }
}
