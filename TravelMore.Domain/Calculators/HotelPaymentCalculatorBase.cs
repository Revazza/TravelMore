using System.IO.Pipes;
using TravelMore.Domain.Common.Enums;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Interfaces;

namespace TravelMore.Domain.Calculators;

public abstract class HotelPaymentCalculatorBase(Hotel hotel, short numberOfDays,PaymentMethod paymentMethod) : IHotelPaymentCalculator
{
    private readonly PaymentMethod _paymentMethod = paymentMethod;
    protected readonly short _numberOfDays = numberOfDays;
    protected readonly Hotel _hotel = hotel;

    public virtual Money Calculate() => _hotel.PricePerDay.Amount * _numberOfDays;

}
