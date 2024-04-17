﻿using TravelMore.Domain.Bookings;
using TravelMore.Domain.Guests;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.PaymentsDetails;

public class BookingPaymentDetails : BasePaymentDetails
{
    public int PayerId { get; private set; }
    public Guest Payer { get; set; } = null!;
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    private BookingPaymentDetails()
    {

    }

    public BookingPaymentDetails(
        PaymentMethod paymentMethod,
        PriceDetails priceDetails,
        Guest payer,
        Guid bookingId) : base(paymentMethod)
    {
        Payer = payer;
        BookingId = bookingId;
        PriceDetails = priceDetails;
    }

}
