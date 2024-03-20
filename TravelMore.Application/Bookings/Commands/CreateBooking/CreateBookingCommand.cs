using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    Guid HotelId,
    short NumberOfGuests,
    DateTime CheckIn,
    DateTime CheckOut) : IRequest<Result<Booking>>;
