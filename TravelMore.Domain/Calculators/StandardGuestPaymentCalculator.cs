using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;

namespace TravelMore.Domain.Calculators;

public class StandardGuestPaymentCalculator : HotelPaymentCalculatorBase
{
    private readonly Money FeePercentage = 0.15m;

    private StandardGuestPaymentCalculator(Hotel hotel, short numberOfDays, PaymentMethod paymentMethod)
        : base(hotel, numberOfDays, paymentMethod)
    {
    }

    public static StandardGuestPaymentCalculator Create(
        Hotel hotel,
        short numberOfDays,
        PaymentMethod paymentMethod)
    {
        EnsureNumberOfDaysIsNotLessOrEqualToZero(numberOfDays);

        return new(hotel, numberOfDays, paymentMethod);
    }

    public override Money Calculate()
    {
        var payment = base.Calculate();
        var fee = CalculateFee(payment);
        var totalPayment = CalculateTotalPayment(payment, fee);

        return payment;
    }

    private static void EnsureNumberOfDaysIsNotLessOrEqualToZero(short numberOfDays)
    {
        //TODO: Create custom exception
        if (numberOfDays.IsLessThanOrEqualToZero())
        {
            throw new Exception();
        }
    }

    private static Money CalculateTotalPayment(Money payment, Money fee) => payment + fee;

    private Money CalculateFee(Money payment) => payment * FeePercentage;

}
