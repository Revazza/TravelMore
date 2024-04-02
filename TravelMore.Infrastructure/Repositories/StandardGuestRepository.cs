using TravelMore.Application.Repositories;
using TravelMore.Domain.Users.StandartGuests;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class StandardGuestRepository : GenericRepository<StandardGuest, int>, IStandardGuestRepository
{
    public StandardGuestRepository(TravelMoreContext context) : base(context)
    {
    }
}
