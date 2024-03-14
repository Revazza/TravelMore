using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Domain.Users.Guests;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class GuestRepository(TravelMoreContext context) : GenericRepository<Guest, int>(context), IGuestRepository
{
}
