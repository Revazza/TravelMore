using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Guests;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.PaymentsDetails;

public class PaymentDetails : Entity<Guid>
{
    public Money TotalPayment { get; init; } = 0;
    public Money Payment { get; init; } = 0;
    public Money Fee { get; init; } = 0;
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentDate { get; init; }
    public int PayerId { get; set; }
    public Guest Payer { get; set; } = null!;
    public int HostId { get; set; }
    public Host Host { get; set; } = null!;

    private PaymentDetails() : base(Guid.NewGuid())
    {

    }

    public PaymentDetails(
        Money totalPayment, 
        Money payment,
        Money fee, 
        PaymentMethod paymentMethod, 
        Guest payer, 
        Host host) : base(Guid.NewGuid())
    {
        TotalPayment = totalPayment;
        Payment = payment;
        Fee = fee;
        PaymentMethod = paymentMethod;
        Payer = payer;
        Host = host;
    }

}
