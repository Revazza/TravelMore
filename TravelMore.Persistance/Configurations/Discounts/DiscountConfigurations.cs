using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Discounts;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Discounts;

public class DiscountConfigurations : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.ComplexProperty(x => x.Value, MoneyConfigurations.DefaultPrecision);

    }
}
