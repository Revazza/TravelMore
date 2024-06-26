﻿using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Extensions;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;
using TravelMore.Domain.Users.Hosts;

namespace TravelMore.Domain.Hotels;

public class Hotel : Entity<Guid>
{
    private readonly List<Booking> _bookings = [];
    private readonly List<PaymentMethod> _acceptedPaymentMethods = [];
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public IReadOnlyCollection<PaymentMethod> AcceptedPaymentMethods => _acceptedPaymentMethods;
    public string Description { get; } = string.Empty;
    public short MaxNumberOfGuests { get; set; }
    public Money PricePerNight { get; set; } = 0;
    public int HostId { get; }
    public Host Host { get; } = null!;
    public Guid DiscountId { get; set; }
    public Discount? Discount { get; set; }

    public Hotel(Guid id) : base(id)
    {

    }

    public Hotel(
        Guid id,
        string description,
        short maxNumberOfGuests,
        Money pricePerNight,
        Host host)
        : base(id)
    {
        Description = description;
        MaxNumberOfGuests = maxNumberOfGuests;
        PricePerNight = pricePerNight;
        Host = host;
        HostId = Host.Id;
    }

    public Money CalculatePriceForNights(int nights)
    {
        var price = PricePerNight * nights;

        if (DiscountExist())
        {
            return Discount!.Apply(price);
        }

        return price;
    }

    public Money ApplyDiscount(Money money)
    {
        if (Discount is null)
        {
            return money;
        }

        return Discount.Apply(money);
    }

    public void SetPricePerDay(decimal price)
    {
        PricePerNight = Money.Create(price);
    }

    public void SetMaxNumberOfGuests(short numberOfGuests)
    {
        if (numberOfGuests.IsLessThanOrEqualToZero())
        {
            throw new HotelInvalidGuestNumberException();
        }

        MaxNumberOfGuests = numberOfGuests;
    }

    public void EnsureBookable(BookingDetails bookingDetails, PaymentMethod paymentMethod)
    {
        EnsureNoBookingsScheduleOverlaps(bookingDetails.Schedule);
        EnsureNumberOfGuestsIsAllowed(bookingDetails.NumberOfGuests);
        EnsureAcceptsPaymentMethod(paymentMethod);
    }

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

    public void EnsureAcceptsPaymentMethod(PaymentMethod paymentMethod)
    {
        //TODO: create custom exception
        if (!AcceptedPaymentMethods.Any(method => method == paymentMethod))
        {
            throw new Exception("Hotel doesn't accept given payment method");
        }
    }

    private bool DiscountExist() => Discount is not null;

    private void EnsureNoBookingsScheduleOverlaps(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            throw new HotelOverlapBookingScheduleException();
        }
    }

    private void EnsureNumberOfGuestsIsAllowed(short numberOfGuests)
    {
        if (!IsNumberOfGuestsAllowed(numberOfGuests))
        {
            throw new Exception();// TODO: create custom exception
        }
    }

    private bool IsNumberOfGuestsAllowed(short numberOfguests) => numberOfguests <= MaxNumberOfGuests;

}
