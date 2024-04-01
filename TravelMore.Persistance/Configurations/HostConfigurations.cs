using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Persistance.Configurations;

public class HostConfigurations : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    }

}
