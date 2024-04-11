using System.IO.Pipes;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Guests;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.PaymentsDetails;

public class BookingPaymentDetails : BasePaymentDetails
{
    public int PayerId { get; private set; }
    public Guest Payer { get; set; } = null!;
    public int HostId { get; set; }
    public Host Host { get; set; } = null!;

    private BookingPaymentDetails() : base(Guid.NewGuid())
    {

    }

    private BookingPaymentDetails(
        Money totalPayment,
        Money payment,
        Money fee,
        PaymentMethod paymentMethod,
        Guest payer,
        Host host) : base(totalPayment, payment, fee, paymentMethod)
    {
        Payer = payer;
        Host = host;
    }

    public static BookingPaymentDetails Create(
        Money totalPayment,
        Money payment,
        Money fee,
        PaymentMethod paymentMethod,
        Guest payer,
        Host host)
    {
        return new(totalPayment, payment, fee, paymentMethod, payer, host);
    }

}
