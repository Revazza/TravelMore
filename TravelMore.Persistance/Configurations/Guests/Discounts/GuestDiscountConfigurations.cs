using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Guests.Discounts;

namespace TravelMore.Persistance.Configurations.Guests.Discounts;

public class GuestDiscountConfigurations : IEntityTypeConfiguration<GuestDiscount>
{
    public void Configure(EntityTypeBuilder<GuestDiscount> builder)
    {
        builder.HasOne(x => x.Guest)
            .WithMany(x => x.Discounts)
            .HasForeignKey(x => x.GuestId);

        builder.HasOne(x => x.Discount)
            .WithMany()
            .HasForeignKey(x => x.DiscountId);

    }
}
