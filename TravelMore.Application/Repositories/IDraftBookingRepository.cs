using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Repositories;

public interface IDraftBookingRepository : IGenericRepository<DraftBooking, Guid>
{
}
