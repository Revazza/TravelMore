using MediatR;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Results;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    Guid HotelId,
    short NumberOfGuests,
    DateTime CheckIn,
    DateTime CheckOut) : IRequest<Result<Booking>>;
