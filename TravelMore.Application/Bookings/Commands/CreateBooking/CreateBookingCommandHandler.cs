using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Models;

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
            return Result.Failure<Booking>(Error.None);
        }

        var hotel = await _hotelRepository.GetHotelByIdWithBookingsAsync(request.HotelId);
        if (hotel is null)
        {
            return Result.Failure<Booking>(Error.None);
        }

        var booking = Booking.Create(
            request.CheckIn,
            request.CheckOut,
            request.NumberOfGuests,
            request.PaymentMethod,
            guest,
            hotel);

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking;
    }

}
