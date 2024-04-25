using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;

namespace TravelMore.Application.Guests.Queries.DoesGuestExistById;

public class DoesGuestExistByIdQueryHandler(IGuestRepository guestRepository) : IRequestHandler<DoesGuestExistByIdQuery, Result<bool>>
{
    private readonly IGuestRepository _guestRepository = guestRepository;

    public async Task<Result<bool>> Handle(DoesGuestExistByIdQuery request, CancellationToken cancellationToken)
    {
        return await _guestRepository.DoesGuestExistById(request.GuestId);
    }
}
