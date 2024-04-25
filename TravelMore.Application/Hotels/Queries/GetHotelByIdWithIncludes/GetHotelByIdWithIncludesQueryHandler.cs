using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelMore.Application.Common.Results;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Hotels.Queries.GetHotelByIdWithIncludes;

public record GetHotelByIdWithIncludesQuery(
    Guid HotelId,
    bool IncludeAcceptedPaymentMethods = false,
    bool IncludeBookings = false,
    bool IncludeDiscount = false,
    bool IncludeHost = false) : IRequest<Result<Hotel?>>;


public class GetHotelByIdWithIncludesQueryHandler(IHotelRepository hotelRepository) : IRequestHandler<GetHotelByIdWithIncludesQuery, Result<Hotel?>>
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;

    public async Task<Result<Hotel?>> Handle(GetHotelByIdWithIncludesQuery request, CancellationToken cancellationToken)
    {
        var includes = GetIncludes(request);

        var query = includes
            .Where(include => include.Key)
            .Aggregate(_hotelRepository.AsQuery(), (hotel, include) => hotel.Include(include.Value));

        var hotel = await query.FirstOrDefaultAsync(hotel => hotel.Id == request.HotelId, cancellationToken);

        return hotel;
    }

    private Dictionary<bool, Expression<Func<Hotel, object>>> GetIncludes(GetHotelByIdWithIncludesQuery request)
        => new()
        {
            { request.IncludeDiscount, hotel => hotel.Discount! },
            { request.IncludeBookings, hotel => hotel.Bookings },
            { request.IncludeAcceptedPaymentMethods, hotel => hotel.AcceptedPaymentMethods },
            { request.IncludeHost, hotel => hotel.Host }
        };

}
