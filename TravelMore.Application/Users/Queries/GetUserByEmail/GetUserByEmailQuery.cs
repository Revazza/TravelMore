using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Users;

namespace TravelMore.Application.Users.Queries.GetUserByEmail;

public record GetUserByEmailQuery(string Email) : IRequest<Result<User?>>;
