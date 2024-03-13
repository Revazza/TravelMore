using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Bookings;

namespace TravelMore.Persistance.Configurations.TravelMore;

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

        builder.OwnsOne(x => x.Schedule);

        builder.OwnsOne(x => x.TotalPayment, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });
    }
}
