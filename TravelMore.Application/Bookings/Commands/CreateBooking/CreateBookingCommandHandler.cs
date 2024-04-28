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
    IDraftBookingRepository draftBookingRepository,
    ISender sender) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IDraftBookingRepository _draftBookingRepository = draftBookingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();
        

        return null;
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
