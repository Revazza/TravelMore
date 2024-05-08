using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Services.Payments;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(Guid DraftBookingId, IPaymentMethodData PaymentData) : IRequest<Result<Booking>>;

