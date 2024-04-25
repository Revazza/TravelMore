using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Guests;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class GuestRepository(TravelMoreContext context) : GenericRepository<Guest, int>(context), IGuestRepository
{
    public async Task<bool> DoesGuestExistByEmail(string email)
        => await _context.Guests.AsNoTracking().AnyAsync(guest => guest.Email == email);

    public async Task<bool> DoesGuestExistById(int id)
        => await _context.Guests.AsNoTracking().AnyAsync(guest => guest.Id == id);
}
