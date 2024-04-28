using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Bookings;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Bookings;

public class DraftBookingConfigurations : IEntityTypeConfiguration<DraftBooking>
{
    public void Configure(EntityTypeBuilder<DraftBooking> builder)
    {
        builder.ComplexProperty(x => x.PriceDetails, priceDetails =>
        {
            priceDetails.ComplexProperty(x => x.DiscountedPrice, MoneyConfigurations.DefaultPrecision);
            priceDetails.ComplexProperty(x => x.InitialPrice, MoneyConfigurations.DefaultPrecision);
            priceDetails.ComplexProperty(x => x.DiscountedAmount, MoneyConfigurations.DefaultPrecision);
        });

    }
}
