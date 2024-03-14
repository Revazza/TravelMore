using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Domain.Hotels;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class HotelRepository(TravelMoreContext context) : GenericRepository<Hotel, Guid>(context), IHotelRepository
{
}
