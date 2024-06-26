﻿using System.Collections.Immutable;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Discounts.Extensions;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Memberships;
using TravelMore.Domain.PaymentsDetails;
using TravelMore.Domain.Users;

namespace TravelMore.Domain.Guests;

public class Guest : User
{
    private readonly List<Booking> _bookings = [];
    private readonly List<BookingPaymentDetails> _bookingPaymentDetails = [];
    private readonly List<Discount> _discounts = [];
    public Money Balance { get; private set; } = 0;
    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; } = null!;
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public IReadOnlyCollection<BookingPaymentDetails> BookingPayments => _bookingPaymentDetails;
    public IReadOnlyCollection<Discount> Discounts => _discounts;

    private Guest() : base(0, string.Empty, string.Empty, string.Empty)
    {

    }

    protected Guest(int id, string email, string passwordHash, string salt, Money balance)
        : base(id, email, passwordHash, salt)
    {
        Balance = balance;
        _discounts = [];
    }

    public Money ApplyDiscounts(Money price, IEnumerable<Guid> discountIds)
    {
        var discounts = GetMembershipDiscounts()
            .Concat(GetFilteredDiscountsByIds(discountIds));

        return discounts.ApplyAll(price);
    }

    public Money ApplyDiscounts(Money price, IEnumerable<Discount> providedDiscounts)
    {
        EnsureHasDiscounts(providedDiscounts.ToList());
        var discounts = GetMembershipDiscounts().Concat(providedDiscounts);

        return discounts.ApplyAll(price);
    }

    public IReadOnlyCollection<Discount> GetAllAvailableDiscounts()
        => GetMembershipDiscounts()
        .Concat(Discounts)
        .ToList()
        .AsReadOnly();

    public void EnsureHasDiscounts(List<Discount> discounts) => discounts.ForEach(EnsureHasDiscount);

    public void EnsureHasDiscount(Discount discount)
    {
        if (!HasDiscount(discount))
        {
            throw new Exception($"The discount with {discount.Id} could not be found for the guest");
        }
    }

    public bool HasDiscount(Discount discount) => Discounts.Contains(discount) || Membership.HasDiscount(discount);

    public virtual void EnsureCanBook(BookingDetails bookingDetails)
    {
        EnsureNoBookingsScheduleOverlaps(bookingDetails.Schedule);
    }

    public void SetBalance(decimal amount)
    {
        Balance = amount;
    }

    public bool AnyBookingsScheduleOverlaps(BookingSchedule schedule) => _bookings.Any(booking => booking.DoesOverLap(schedule.From, schedule.To));

    public void EnsureNoBookingsScheduleOverlaps(BookingSchedule schedule)
    {
        if (AnyBookingsScheduleOverlaps(schedule))
        {
            throw new GuestOverlapBookingScheduleException();
        }
    }

    public IReadOnlyCollection<Discount> GetMembershipDiscounts() => Membership.Discounts;

    public IReadOnlyCollection<Discount> GetFilteredDiscountsByIds(IEnumerable<Guid> discountIds)
        => _discounts
        .Where(discount => discountIds.Any(discountId => discountId == discount.Id))
        .ToList()
        .AsReadOnly();

    protected void EnsureBalanaceIsEnough(Money payment)
    {
        if (!IsBalanceEnough(payment))
        {
            throw new GuestInsufficientBalanceException();
        }
    }

    protected bool IsBalanceEnough(Money totalPayment) => Balance >= totalPayment;

}
