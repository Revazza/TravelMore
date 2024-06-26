﻿using TravelMore.Domain.Bookings.Enums;
using TravelMore.Domain.Bookings.ValueObjects;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Users.Hosts.Exceptions;

namespace TravelMore.Domain.Bookings;

public abstract class Booking : Entity<Guid>
{
    protected readonly List<Discount> _appliedDiscounts;
    public IReadOnlyCollection<Discount> AppliedDiscounts => _appliedDiscounts;
    public BookingDetails Details { get; private set; }
    public int GuestId { get; init; }
    public Guest Guest { get; init; } = null!;
    public Guid HotelId { get; init; }
    public Hotel Hotel { get; init; }
    public BookingStatus Status { get; protected set; }

    protected Booking(Guid id) : base(id) { }

    /// <summary>
    /// Private consturctor needed to create database migrations
    /// </summary>
    protected Booking() : base(Guid.NewGuid())
    {
        Details = null!;
        Hotel = null!;
        _appliedDiscounts = [];
    }


    protected Booking(
        Guid id,
        BookingDetails details,
        Guest guest,
        Hotel hotel,
        List<Discount> appliedDiscounts) : base(id)
    {
        Status = BookingStatus.Pending;
        Guest = guest;
        Hotel = hotel;
        Details = details;
        _appliedDiscounts = appliedDiscounts;
    }

    public bool DoesOverLap(DateTime from, DateTime to) => Details.Schedule.From <= to && from <= Details.Schedule.To;

    protected void EnsureGuestIdMatches(int guestId)
    {
        if (Guest.Id != guestId)
        {
            throw new GuestIdMismatchedException();
        }
    }

    protected void EnsureHotelHostIdMatches(int hostId)
    {
        if (Hotel.Host.Id != hostId)
        {
            throw new HostIdMismatchedException();
        }
    }

}
