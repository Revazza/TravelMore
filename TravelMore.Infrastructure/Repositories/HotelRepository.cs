using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Hotels;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class HotelRepository(TravelMoreContext context) : GenericRepository<Hotel, Guid>(context), IHotelRepository
{
    public async Task<Hotel?> GetHotelByIdWithBookingsAsync(Guid hotelId)
    {
        return await _context
            .Hotels
            .Include(h => h.Bookings)
            .FirstOrDefaultAsync(x => x.Id == hotelId);
    }
}
