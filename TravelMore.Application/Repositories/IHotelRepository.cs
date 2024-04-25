using TravelMore.Domain.Hotels;

namespace TravelMore.Application.Repositories;

public interface IHotelRepository : IGenericRepository<Hotel, Guid>
{
    Task<Hotel?> GetHotelByIdWithBookingsAsync(Guid hotelId);
    Task<bool> DoesHotelExistsByIdAsync(Guid hotelId);
}
