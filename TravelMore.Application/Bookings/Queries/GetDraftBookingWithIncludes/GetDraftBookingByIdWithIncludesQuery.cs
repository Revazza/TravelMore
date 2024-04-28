using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Bookings.Queries.GetDraftBookingWithIncludes;

public record GetDraftBookingByIdWithIncludesQuery(
    Guid DraftBookingId,
    bool IncludeAppliedDiscounts = false,
    bool IncludeGuest = false,
    bool IncludeHotel = false) : IRequest<Result<DraftBooking?>>;
