using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;

namespace TravelMore.Application.Hotels.Queries.DoesHotelExistsById;

public class DoesHotelExistsByIdQueryHandler(IHotelRepository hotelRepository) : IRequestHandler<DoesHotelExistsByIdQuery, Result<bool>>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;

    public async Task<Result<bool>> Handle(DoesHotelExistsByIdQuery request, CancellationToken cancellationToken)
    {
        return await _hotelRepository.DoesHotelExistsByIdAsync(request.HotelId);
    }
}
