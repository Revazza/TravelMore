using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Bookings;

namespace TravelMore.Persistance.Configurations;

public class BookingConfigurations : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.Guest)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.GuestId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.BookedHotel)
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.BookedHotelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ComplexProperty(x => x.Details);

        builder.ComplexProperty(x => x.Details)
            .ComplexProperty(x => x.Schedule);

    }
}
