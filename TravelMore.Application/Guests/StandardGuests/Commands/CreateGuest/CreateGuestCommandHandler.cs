using Mapster;
using MediatR;
using TravelMore.Application.Common.Dtos;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Application.Services.PasswordHasher;
using TravelMore.Application.Users.Queries.DoesUserExistByEmail;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Errors;
using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Application.Guests.StandardGuests.Commands.CreateGuest;

public class CreateGuestCommandHandler(
    IPasswordHasher passwordHasher,
    ISender sender,
    IStandardGuestRepository standardGuestRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGuestCommand, Result<StandardGuestDto>>
{
    private readonly IStandardGuestRepository _standardGuestRepository = standardGuestRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ISender _sender = sender;

    public async Task<Result<StandardGuestDto>> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
    {
        var guestExists = await _sender.Send(new DoesUserExistByEmailQuery(request.Username), cancellationToken);

        if (guestExists.Value)
        {
            return DomainErrors.Guest.AlreadyExistByEmail;
        }

        var hashPasswordResponse = _passwordHasher.Hash(request.Password);

        var guest = new StandardGuest(request.Username, hashPasswordResponse.HashedPassword, hashPasswordResponse.Salt, Money.Default);

        await _standardGuestRepository.AddAsync(guest);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return guest.Adapt<StandardGuestDto>();
    }
}
