using MediatR;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.Guests.Queries.DoesGuestExistByEmail;

public record DoesGuestExistByEmailQuery(string Email) : IRequest<Result<bool>>;
