using TravelMore.Domain.Common.Models;
using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingPaymentInfo(
    Money TotalPayment,
    Money HotelPayment,
    Money Fee,
    PaymentMethod PaymentMethod);
