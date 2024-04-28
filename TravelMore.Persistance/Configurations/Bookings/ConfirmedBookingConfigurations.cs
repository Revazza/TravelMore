using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Bookings;

namespace TravelMore.Persistance.Configurations.Bookings;

public class ConfirmedBookingConfigurations : IEntityTypeConfiguration<ConfirmedBooking>
{
    public void Configure(EntityTypeBuilder<ConfirmedBooking> builder)
    {
        builder.HasOne(x => x.Payment)
            .WithOne()
            .HasForeignKey<ConfirmedBooking>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
