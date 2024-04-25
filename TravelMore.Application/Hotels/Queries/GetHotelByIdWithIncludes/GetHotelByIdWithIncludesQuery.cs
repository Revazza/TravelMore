using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Hotels.Queries.GetHotelByIdWithIncludes;

public record GetHotelByIdWithIncludesQuery(
    Guid HotelId,
    bool IncludeAcceptedPaymentMethods = false,
    bool IncludeBookings = false,
    bool IncludeDiscount = false,
    bool IncludeHost = false) : IRequest<Result<Hotel?>>;
