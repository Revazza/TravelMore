using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Hotels.Queries.GetHotelById;

public record GetHotelByIdQuery(Guid Id) : IRequest<Result<Hotel>>;
