using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Guests.Queries.DoesGuestExistById;
using TravelMore.Application.Hotels.Queries.DoesHotelExistsById;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Guests.Exceptions;
using TravelMore.Domain.Hotels.Exceptions;
using TravelMore.Domain.Hotels;
using TravelMore.Domain.Guests;
using TravelMore.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TravelMore.Application.Bookings.Commands.CreateDraftBooking;

public class CreateDraftBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IDraftBookingRepository draftBookingRepository,
    IGuestRepository guestRepository,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    ISender sender) : IRequestHandler<CreateDraftBookingCommand, Result<DraftBooking>>
{
    private readonly IDraftBookingRepository _draftBookingRepository = draftBookingRepository;
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<DraftBooking>> Handle(CreateDraftBookingCommand request, CancellationToken cancellationToken)
    {
        var guestId = _userIdentityService.GetUserId();
        await EnsureGuestAndHotelExistsAsync(guestId, request.HotelId);

        var guest = await GetGuestAsync(guestId);
        var hotel = await GetHotelAsync(request.HotelId);

        var draftBooking = DraftBooking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            request.PaymentMethod,
            guest,
            hotel,
            request.AppliedDiscountIds);

        await _draftBookingRepository.AddAsync(draftBooking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return draftBooking;
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
        var guest = await _guestRepository
            .AsQuery()
            .Include(guest => guest.Discounts)
            .Include(guest => guest.Bookings)
            .Include(guest => guest.Membership)
            .FirstOrDefaultAsync(guest => guest.Id == guestId);

        return guest!;
    }

    private async Task<Hotel> GetHotelAsync(Guid hotelId)
    {
        var hotel = await _hotelRepository
            .AsQuery()
            .Include(hotel => hotel.AcceptedPaymentMethods)
            .Include(hotel => hotel.Bookings)
            .Include(hotel => hotel.Discount)
            .FirstOrDefaultAsync(hotel => hotel.Id == hotelId);

        return hotel!;
    }
}
