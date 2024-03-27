using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Bookings.ValueObjects;

public record BookingPaymentInfo(
    Money TotalPayment,
    Money HotelPayment,
    Money Fee,
    PaymentMethod PaymentMethod);
