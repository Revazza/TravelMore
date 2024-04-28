using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    Guid DraftBookingId) : IRequest<Result<Booking>>;
