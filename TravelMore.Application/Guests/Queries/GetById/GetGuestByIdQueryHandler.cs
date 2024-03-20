using MediatR;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Users.Guests;

namespace TravelMore.Application.Guests.Queries.GetById;

public class GetGuestByIdQueryHandler(IGuestRepository guestRepository) : IRequestHandler<GetGuestByIdQuery, Result<Guest>>
{
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<Result<Guest>> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
    {
        var guest = await _guestRepository.GetByIdAsync(request.Id);

        return Result.Success(guest);
    }
}
