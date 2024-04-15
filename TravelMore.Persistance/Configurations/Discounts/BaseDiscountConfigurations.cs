using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Discounts;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations.Discounts;

public class BaseDiscountConfigurations : IEntityTypeConfiguration<BaseDiscount>
{
    public void Configure(EntityTypeBuilder<BaseDiscount> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.ComplexProperty(x => x.Amount, MoneyConfigurations.DefaultPrecision);

    }
}
