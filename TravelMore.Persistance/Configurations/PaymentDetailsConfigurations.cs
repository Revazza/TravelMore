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
            .WithMany(x => x.BookingPayments)
            .HasForeignKey(x => x.PayerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ComplexProperty(x => x.PriceDetails, priceDetails =>
        {
            priceDetails.ComplexProperty(x => x.ActualPayment, MoneyConfigurations.DefaultPrecision);
            priceDetails.ComplexProperty(x => x.InitialPrice, MoneyConfigurations.DefaultPrecision);
            priceDetails.ComplexProperty(x => x.DiscountedPrice, MoneyConfigurations.DefaultPrecision);
        });


    }

}
