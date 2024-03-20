using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Application.Guests.Queries.GetById;

public record GetGuestByIdQuery(int Id) : IRequest<Result<Guest>>;
