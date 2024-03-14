using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Domain.Users.Hosts;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class HostRepository(TravelMoreContext context) : GenericRepository<Host, int>(context), IHostRepository
{
}
