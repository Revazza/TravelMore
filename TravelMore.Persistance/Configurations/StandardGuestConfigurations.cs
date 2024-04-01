using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Persistance.Configurations;

public class StandardGuestConfigurations : IEntityTypeConfiguration<StandardGuest>
{
    public void Configure(EntityTypeBuilder<StandardGuest> builder)
    {
    }
}
