using MediatR;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.Users.Queries.DoesUserExistByEmail;

public record DoesUserExistByEmailQuery(string Email) : IRequest<Result<bool>>;
