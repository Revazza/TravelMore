using MediatR;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.Guests.Queries.DoesGuestExistById;

public record DoesGuestExistByIdQuery(int GuestId) : IRequest<Result<bool>>;
