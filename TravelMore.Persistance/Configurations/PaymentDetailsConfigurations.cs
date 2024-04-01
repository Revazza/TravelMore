using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails;

namespace TravelMore.Persistance.Configurations;

public class PaymentDetailsConfigurations : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
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

        builder.ComplexProperty(x => x.TotalPayment, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });
        builder.ComplexProperty(x => x.Payment, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });
        builder.ComplexProperty(x => x.Fee, price =>
        {
            price.Property(x => x.Amount)
                .HasPrecision(18, 10);
        });

    }

}
