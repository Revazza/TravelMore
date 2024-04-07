using MediatR;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.Users.Queries.CheckPassword;

public record CheckPasswordQuery(string Password, string Salt, string HashedPassword) : IRequest<Result<bool>>;
