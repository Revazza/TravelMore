using TravelMore.Application.Repositories;
using TravelMore.Domain.Bookings;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class DraftBookingRepository : GenericRepository<DraftBooking, Guid>, IDraftBookingRepository
{
    public DraftBookingRepository(TravelMoreContext context) : base(context)
    {
    }
}
