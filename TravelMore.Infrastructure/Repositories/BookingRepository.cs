using TravelMore.Application.Repositories;
using TravelMore.Domain.Bookings;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class BookingRepository(TravelMoreContext context) : GenericRepository<Booking, Guid>(context), IBookingRepository
{
}
