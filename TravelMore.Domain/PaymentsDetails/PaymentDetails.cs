using TravelMore.Domain.Calculators;
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
    public PaymentMethod PaymentMethod { get; init; }
    public DateTime PaymentDate { get; init; }

    private PaymentDetails() : base(Guid.NewGuid())
    {

    }

    public static PaymentDetails Create(
        PaymentMethod paymentMethod,
        Guest guest,
        Hotel hotel)
    {

        //var totalPayment = PremiumGuestPaymentCalculator.Create().Calculate();
        //var totalPayment2 = StandardGuestPaymentCalculator.Create().Calculate();

        return new();
    }


}
