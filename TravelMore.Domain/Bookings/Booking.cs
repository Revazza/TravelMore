﻿using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Calculators;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Discounts.Calculators;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.PaymentsDetails.Enums;
using TravelMore.Domain.PaymentsDetails.ValueObjects;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Bookings;

public sealed class Booking : Entity<Guid>
{
    public BookingDetails Details { get; private set; }
    public BookingPaymentDetails? Payment { get; private set; }
    public int GuestId { get; private set; }
    public Guest Guest { get; private set; } = null!;
    public Guid BookedHotelId { get; private set; }
    public Hotel BookedHotel { get; private set; }
    public BookingStatus Status { get; private set; }
    public List<Discount> AppliedDiscounts { get; set; }


    /// <summary>
    /// Private consturctor needed to create database migrations
    /// </summary>
    private Booking() : base(Guid.NewGuid())
    {
        Details = null!;
        BookedHotel = null!;
        Payment = null;
        AppliedDiscounts = [];
    }


    private Booking(
        BookingPaymentDetails payment,
        BookingDetails details,
        Guest guest,
        Hotel bookedHotel) : base(Guid.NewGuid())
    {
        Details = details;
        Status = BookingStatus.Pending;
        Guest = guest;
        BookedHotel = bookedHotel;
        Payment = payment;
    }

    public static Booking Create(
       DateTime from,
       DateTime to,
       short numberOfGuests,
       PaymentMethod paymentMethod,
       Guest guest,
       Hotel hotel,
       List<Discount> guestDiscounts)
    {
        var schedule = BookingSchedule.Create(from, to);
        var numberOfNights = schedule.GetDurationInDays();

        var bookingDetails = new BookingDetails(
            numberOfGuests,
            numberOfNights,
            schedule);

        hotel.EnsureBookable(bookingDetails, paymentMethod);
        guest.EnsureCanBook(bookingDetails);
        guest.EnsureHasDiscounts(guestDiscounts);

        AddHotelDiscountIfExists(guestDiscounts, hotel.Discount);
        var priceDetails = CalculatePriceDetails(hotel.GetPriceForNights(numberOfNights), guestDiscounts);
        var payment = new BookingPaymentDetails(priceDetails, paymentMethod, guest);

        return new(payment, bookingDetails, guest, hotel);
    }

    private static PriceDetails CalculatePriceDetails(Money price, List<Discount> discounts)
    {
        var discountedPrice = new DiscountsApplier(discounts).Apply(price);
        var discountedAmount = price - discountedPrice;

        return new(price, discountedAmount, discountedPrice);
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

    public void Cancel(int guestId)
    {
        EnsureGuestIdMatches(guestId);
        Status = BookingStatus.Canceled;
    }

    public void SetPaymentDetails(BookingPaymentDetails paymentDetails)
    {
        Payment = paymentDetails;
    }

    public bool IsPaymentMethodMatching(PaymentMethod paymentMethod) => paymentMethod == Payment!.PaymentMethod;

    public bool DoesOverLap(DateTime from, DateTime to) => Details.Schedule.From <= to && from <= Details.Schedule.To;

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

    private void EnsureNotAccepted()
    {
        //TODO: create custom exception 
        if (Status == BookingStatus.Accepted)
        {
            throw new Exception();
        }
    }

    private void EnsureGuestIdMatches(int guestId)
    {
        if (Guest.Id != guestId)
        {
            throw new GuestIdMismatchedException();
        }
    }

    private void EnsureHotelHostIdMatches(int hostId)
    {
        if (BookedHotel.Host.Id != hostId)
        {
            throw new HostIdMismatchedException();
        }
    }

    private static void AddHotelDiscountIfExists(List<Discount> discounts, Discount? hotelDiscount)
    {
        if (hotelDiscount is not null)
        {
            discounts.Add(hotelDiscount);
        }
    }

}
