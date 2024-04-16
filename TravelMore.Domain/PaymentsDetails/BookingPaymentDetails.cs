using TravelMore.Domain.Guests;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.PaymentsDetails;

public class BookingPaymentDetails : BasePaymentDetails
{
    public int PayerId { get; private set; }
    public Guest Payer { get; set; } = null!;
    public int HostId { get; set; }
    public Host Host { get; set; } = null!;

    private BookingPaymentDetails()
    {

    }

    public BookingPaymentDetails(
        PriceDetails priceDetails,
        PaymentMethod paymentMethod,
        Guest payer) : base(paymentMethod)
    {
        Payer = payer;
        Host = null!;
        PriceDetails = priceDetails;
    }

}
