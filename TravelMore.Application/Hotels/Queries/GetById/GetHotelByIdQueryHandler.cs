using MediatR;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Domain.Common.Results;
using TravelMore.Domain.Errors;
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
            return Result.Failure<Hotel>(DomainErrors.Hotel.NotFound);
        }

        return Result.Success(hotel);

    }
}
