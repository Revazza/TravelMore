using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelMore.Domain.Common.Models;

namespace TravelMore.Persistance.Configurations.Common;

public static class MoneyConfigurations
{
    public static void DefaultPrecision(this ComplexPropertyBuilder<Money> builder)
        => builder.Property(x => x.Amount).HasPrecision(18, 10);

}
