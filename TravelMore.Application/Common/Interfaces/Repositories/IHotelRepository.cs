using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Common.Interfaces.Repositories;

public interface IHotelRepository : IGenericRepository<Hotel, Guid>
{
    Task<Hotel?> GetHotelByIdWithBookingsAsync(Guid hotelId);
}
