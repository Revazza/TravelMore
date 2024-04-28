using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Guests;

namespace TravelMore.Application.Guests.Queries.GetGuestByIdWithIncludes;

public record GetGuestByIdWithIncludesQuery(
    int GuestId,
    bool IncludeDiscounts = false,
    bool IncludeMembership = false,
    bool IncludeBookings = false,
    bool IncludeBookingPayments = false) : IRequest<Result<Guest?>>;
