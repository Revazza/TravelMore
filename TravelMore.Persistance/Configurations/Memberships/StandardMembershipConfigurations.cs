using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Memberships;

namespace TravelMore.Persistance.Configurations.Memberships;

public class StandardMembershipConfigurations : IEntityTypeConfiguration<StandardMembership>
{
    public void Configure(EntityTypeBuilder<StandardMembership> builder)
    {

    }
}
