using MediatR;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Application.Common.Interfaces.Services;
using TravelMore.Application.Guests.Queries.GetById;
using TravelMore.Application.Hotels.Queries.GetById;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Results;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    Guid HotelId,
    short NumberOfGuests,
    DateTime CheckIn,
    DateTime CheckOut) : IRequest<Result<Booking>>;

public class CreateBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IBookingRepository bookingRepository,
    ISender sender,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ISender _sender = sender;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guestResult = await _sender.Send(new GetGuestByIdQuery(_userIdentityService.GetUserId()), cancellationToken);
        if (guestResult.IsFailure)
        {
            return Result<Booking>.Failure(guestResult.Error);
        }

        var hotelResult = await _sender.Send(new GetHotelByIdQuery(request.HotelId), cancellationToken);
        if (hotelResult.IsFailure)
        {
            return Result<Booking>.Failure(hotelResult.Error);
        }

        var bookingResult = Booking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            guestResult.Value,
            hotelResult.Value);

        if (bookingResult.IsFailure)
        {
            return Result<Booking>.Failure(bookingResult.Error);
        }

        await _bookingRepository.AddAsync(bookingResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookingResult;
    }
}
