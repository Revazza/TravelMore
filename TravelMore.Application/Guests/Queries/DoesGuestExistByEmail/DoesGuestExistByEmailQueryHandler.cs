using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;

namespace TravelMore.Application.Guests.Queries.DoesGuestExistByEmail;

public class DoesGuestExistByEmailQueryHandler : IRequestHandler<DoesGuestExistByEmailQuery, Result<bool>>
{
    private readonly IGuestRepository _guestRepository;

    public DoesGuestExistByEmailQueryHandler(IGuestRepository guestRepository)
    {
        _guestRepository = guestRepository;
    }

    public async Task<Result<bool>> Handle(DoesGuestExistByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _guestRepository.DoesGuestExistByEmail(request.Email);
    }

}
