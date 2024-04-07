using MediatR;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Common.Models;
using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Hotels.Queries.GetHotelById;

public class GetHotelByIdQueryHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelByIdQuery, Result<Hotel>>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;

    public async Task<Result<Hotel>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.Id);
        if (hotel is null)
        {
            return Error.None;
        }

        return hotel;

    }
}
