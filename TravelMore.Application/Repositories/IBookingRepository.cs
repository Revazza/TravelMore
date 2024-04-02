using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Repositories;

public interface IBookingRepository : IGenericRepository<Booking, Guid>
{
}
