using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Users.StandartGuests;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class StandardGuestRepository : GenericRepository<StandardGuest, int>, IStandardGuestRepository
{
    public StandardGuestRepository(TravelMoreContext context) : base(context)
    {
    }

    public async Task<StandardGuest?> GetByEmailAsync(string email)
        => await _context.StandardGuests.FirstOrDefaultAsync(guest => guest.Email == email);
}
