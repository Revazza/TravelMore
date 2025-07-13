using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Domain.Bookings;

public class ConfirmedBooking : Booking
{
    public BookingPaymentDetails Payment { get; private set; }

    public static ConfirmedBooking Create(
        DraftBooking draftBooking)
    {
            
        // Something

        return null;
    }

    public void Accept(int hostId)
    {
        EnsureHotelHostIdMatches(hostId);
        EnsurePaymentCompleted();
        Status = BookingStatus.Accepted;
    }

    public void Decline(int hostId)
    {
        EnsureHotelHostIdMatches(hostId);
        EnsureNotAccepted();
        Status = BookingStatus.Declined;
    }

    private void EnsureNotAccepted()
    {
        //TODO: create custom exception 
        if (Status == BookingStatus.Accepted)
        {
            throw new Exception();
        }
    }

    public void EnsurePaymentCompleted()
    {
        //TODO: create custom exception 

        if (Payment is null)
        {
            throw new Exception("Payment is not provided");
        }
        if (Payment.PaymentStatus != PaymentStatus.Completed)
        {
            throw new Exception("Payment status is not completed");
        }
    }

}
