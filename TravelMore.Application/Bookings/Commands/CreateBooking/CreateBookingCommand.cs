using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.Common.Enums;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(
    short NumberOfGuests,
    DateTime CheckIn,
    DateTime CheckOut,
    PaymentMethod PaymentMethod,
    Guid HotelId) : IRequest<Result<Booking>>;
