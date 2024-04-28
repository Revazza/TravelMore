using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Guests.Queries.DoesGuestExistById;
using TravelMore.Application.Guests.Queries.GetGuestByIdWithIncludes;
using TravelMore.Application.Hotels.Queries.DoesHotelExistsById;
using TravelMore.Application.Hotels.Queries.GetHotelByIdWithIncludes;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Guests;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Hotels.Exceptions;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    ISender sender) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();
        await EnsureGuestAndHotelExistsAsync(guestId, request.HotelId);

        var guest = await GetGuestAsync(guestId);
        var hotel = await GetHotelAsync(request.HotelId);

        var booking = Booking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            request.PaymentMethod,
            guest,
            hotel,
            request.AppliedDiscountIds);

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking;
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
    {
        var query = new GetGuestByIdWithIncludesQuery(
            guestId,
            IncludeDiscounts: true,
            IncludeBookings: true,
            IncludeBookingPayments: true,
            IncludeMembership: true);

        var guestResult = await _sender.Send(query);

        return guestResult.Value!;
    }

    private async Task<Hotel> GetHotelAsync(Guid hotelId)
    {
        var query = new GetHotelByIdWithIncludesQuery(
            hotelId,
            IncludeAcceptedPaymentMethods: true,
            IncludeBookings: true,
            IncludeDiscount: true,
            IncludeHost: true);

        var hotelResult = await _sender.Send(query);

        return hotelResult.Value!;
    }

}
