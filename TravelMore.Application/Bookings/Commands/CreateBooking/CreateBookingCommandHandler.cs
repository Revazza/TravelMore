using MediatR;
using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Discounts.Commands.ApplyDiscounts;
using TravelMore.Application.Guests.Queries.DoesGuestExistByEmail;
using TravelMore.Application.Guests.Queries.DoesGuestExistById;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Discounts;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Hotels;

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
        var guest = await _guestRepository
            .AsQuery()
            .Include(guest => guest.Discounts)
            .Include(guest => guest.Membership)
            .Include(guest => guest.Bookings)
            .Include(guest => guest.BookingPayments)
            .FirstOrDefaultAsync(guest => guest.Id == _userIdentityService.GetUserId());

        if (guest is null)
        {
            return Result.Failure<Booking>(Error.None);
        }

        var hotel = await _hotelRepository.GetHotelByIdWithBookingsAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<Booking>(Error.None);
        }

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

    private async Task EnsureGuestExists(int guestId)
    {
        var guestExistsResult = await _sender.Send(new DoesGuestExistByIdQuery(guestId));

        if (!guestExistsResult.IsSuccess)
        {
            throw new 
        }
    }

}
