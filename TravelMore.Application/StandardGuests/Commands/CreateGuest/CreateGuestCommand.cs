using MediatR;
using TravelMore.Application.Common.Dtos;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.StandardGuests.Commands.CreateGuest;

public record CreateGuestCommand(
    string Username,
    string Password) : IRequest<Result<StandardGuestDto>>;
