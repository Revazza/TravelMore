using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Discounts.Commands.ApplyDiscounts;
using TravelMore.Application.Guests.Queries.DoesGuestExistById;
using TravelMore.Application.Hotels.Queries.DoesHotelExistsById;
using TravelMore.Application.Hotels.Queries.GetHotelByIdWithIncludes;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository,
    IGuestRepository guestRepository,
    ISender sender) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();
        await EnsureGuestAndHotelExistsAsync(guestId, request.HotelId);

        var guest = await GetGuestAsync(guestId);
        var hotel = await GetHotelAsync(request.HotelId);

        var discounts = GetDiscounts(guest, hotel);
        var priceDetails = _sender.Send(new ApplyDiscountsCommand(Money.Default, discounts), cancellationToken);


        var booking = Booking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            request.PaymentMethod,
            guest,
            hotel,
            discounts);

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking;
    }

    private static List<Discount> GetDiscounts(Guest guest, Hotel hotel)
    {
        var discounts = new List<Discount>();

        if (hotel.Discount is not null)
        {
            discounts.Add(hotel.Discount);
        }

        discounts.AddRange(guest.Discounts);
        return discounts;
    }

    private async Task EnsureGuestAndHotelExistsAsync(int guestId, Guid hotelId)
    {
        await EnsureGuestExistsAsync(guestId);
        await EnsureHotelExistsAsync(hotelId);
    }

    private async Task EnsureGuestExistsAsync(int guestId)
    {
        var guestExistsResult = await _sender.Send(new DoesGuestExistByIdQuery(guestId));

        if (guestExistsResult.IsFailure)
        {
            throw new GuestNotFoundByIdException(guestId);
        }
    }

    private async Task EnsureHotelExistsAsync(Guid hotelId)
    {
        var hotelExistsResult = await _sender.Send(new DoesHotelExistsByIdQuery(hotelId));

        if (!hotelExistsResult.Value)
        {
            throw new HotelNotFoundException();
        }
    }

    private async Task<Guest> GetGuestAsync(int guestId)
        => await _guestRepository
        .AsQuery()
        .Include(guest => guest.Discounts)
        .Include(guest => guest.Membership)
        .Include(guest => guest.Bookings)
        .Include(guest => guest.BookingPayments)
        .FirstAsync(guest => guest.Id == guestId);

    private async Task<Hotel> GetHotelAsync(Guid hotelId)
    {
        var hotelResult = await _sender.Send(new GetHotelByIdWithIncludesQuery(hotelId, IncludeAcceptedPaymentMethods: true, IncludeBookings: true, IncludeDiscount: true, IncludeHost: true));
        return hotelResult.Value!;
    }

}
