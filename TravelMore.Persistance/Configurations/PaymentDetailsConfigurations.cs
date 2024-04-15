using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Persistance.Configurations.Common;

namespace TravelMore.Persistance.Configurations;

public class PaymentDetailsConfigurations : IEntityTypeConfiguration<BookingPaymentDetails>
{
    public void Configure(EntityTypeBuilder<BookingPaymentDetails> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.Payer)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PayerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Host)
            .WithMany(x => x.ReceivedPayments)
            .HasForeignKey(x => x.HostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ComplexProperty(x => x.TotalPayment, MoneyConfigurations.DefaultPrecision);
        builder.ComplexProperty(x => x.Payment, MoneyConfigurations.DefaultPrecision);
        builder.ComplexProperty(x => x.Fee, MoneyConfigurations.DefaultPrecision);

    }

}
