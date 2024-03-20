using MediatR;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Application.Common.Results;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Hotels.Queries.GetById;

public record GetHotelByIdQuery(Guid Id) : IRequest<Result<Hotel>>;

public class GetHotelByIdQueryHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelByIdQuery, Result<Hotel>>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;

    public async Task<Result<Hotel>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);
        if (hotel is null)
        {
            return Result.Failure<Hotel>(Error.None);
        }

        return Result.Success(hotel);

    }
}
