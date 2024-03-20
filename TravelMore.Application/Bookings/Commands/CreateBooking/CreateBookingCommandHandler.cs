using MediatR;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Application.Common.Interfaces.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Results;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IUserIdentityService userIdentityService,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository,
    IGuestRepository guestRepository) : IRequestHandler<CreateBookingCommand, Result<Booking>>
{
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IGuestRepository _guestRepository = guestRepository;
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var guest = await _guestRepository.GetByIdAsync(_userIdentityService.GetUserId());

        if (guest is null)
        {
            return Result.Failure<Booking>(DomainErrors.Guest.NotFound);
        }

        var hotel = await _hotelRepository.GetHotelByIdWithBookingsAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<Booking>(DomainErrors.Hotel.NotFound);
        }

        var bookingResult = Booking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            guest,
            hotel);

        if (bookingResult.IsFailure)
        {
            return Result.Failure<Booking>(bookingResult.Error);
        }

        await _bookingRepository.AddAsync(bookingResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookingResult;
    }
}
