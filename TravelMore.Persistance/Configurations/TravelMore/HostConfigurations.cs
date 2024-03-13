using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Persistance.Configurations.TravelMore;

public class HostConfigurations : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasData(SeedHost);

    }

    public static readonly dynamic SeedHost = new { Id = 2, Email = "host@gmail.com" };

}
