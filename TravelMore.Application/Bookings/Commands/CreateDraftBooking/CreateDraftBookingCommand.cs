using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;
using TravelMore.Domain.PaymentsDetails.Enums;

namespace TravelMore.Application.Bookings.Commands.CreateDraftBooking;

public record CreateDraftBookingCommand(
    short NumberOfGuests,
    DateTime CheckIn,
    DateTime CheckOut,
    PaymentMethod PaymentMethod,
    Guid HotelId,
    List<Guid> AppliedDiscountIds) : IRequest<Result<DraftBooking>>;
