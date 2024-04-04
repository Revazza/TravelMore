using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Users.Guests;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class GuestRepository(TravelMoreContext context) : GenericRepository<Guest, int>(context), IGuestRepository
{
    public async Task<bool> DoesGuestExistByEmail(string email) 
        => await _context.Guests.AnyAsync(guest => guest.Email == email);

}
