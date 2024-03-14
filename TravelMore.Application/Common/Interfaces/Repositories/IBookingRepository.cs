using TravelMore.Domain.Bookings;

namespace TravelMore.Application.Common.Interfaces.Repositories;

public interface IBookingRepository : IGenericRepository<Booking, Guid>
{
}
