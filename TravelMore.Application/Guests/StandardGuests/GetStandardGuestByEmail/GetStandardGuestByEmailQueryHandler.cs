using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Users.StandartGuests;
using TravelMore.Domain.Errors;


namespace TravelMore.Application.Guests.StandardGuests.GetStandardGuestByEmail;

public record GetStandardGuestByEmailQuery(string Email) : IRequest<Result<StandardGuest?>>;

public class GetStandardGuestByEmailQueryHandler(IStandardGuestRepository standardGuestRepository) : IRequestHandler<GetStandardGuestByEmailQuery, Result<StandardGuest?>>
{
    private readonly IStandardGuestRepository _standardGuestRepository = standardGuestRepository;

    public async Task<Result<StandardGuest?>> Handle(GetStandardGuestByEmailQuery request, CancellationToken cancellationToken)
    {
        var guest = await _standardGuestRepository.GetByEmailAsync(request.Email);
        if (guest is null)
        {
            return DomainErrors.Guest.NotFoundByEmail;
        }

        return guest;
    }
}
