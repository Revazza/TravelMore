using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;

namespace TravelMore.Application.Guests.Queries.DoesGuestExistByUsername;

public record DoesGuestExistByUsernameQuery(string Username) : IRequest<Result<bool>>;

public class DoesGuestExistByUsernameQueryHandler : IRequestHandler<DoesGuestExistByUsernameQuery, Result<bool>>
{
    private readonly IGuestRepository _guestRepository;

    public DoesGuestExistByUsernameQueryHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<bool>> Handle(DoesGuestExistByUsernameQuery request, CancellationToken cancellationToken)
    {
        return await _guestRepository.DoesGuestExistByUsername(request.Username);
    }

}
