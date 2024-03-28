using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Domain.PaymentsDetails;

public class PaymentDetails : Entity<Guid>
{
    public Money TotalPayment { get; init; } = 0;
    public Money Payment { get; init; } = 0;
    public Money Fee { get; init; } = 0;
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime PaymentDate { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PayerId { get; set; }
    public Guest Payer { get; set; } = null!;
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public PaymentDetails(PaymentMethod paymentMethod) : base(Guid.NewGuid())
    {
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.UtcNow;
    }

    private PaymentDetails() : base(Guid.NewGuid())
    {

    }

    public static PaymentDetails Create(
        short numberOfDays,
        PaymentMethod paymentMethod,
        Guest guest,
        Hotel hotel)
    {
        var fee = 0;
        var payment = 0;
        var totalPayment = 0;


        return new();
    }

}
