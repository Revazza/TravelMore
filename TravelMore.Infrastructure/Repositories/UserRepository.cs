using Microsoft.EntityFrameworkCore;
using TravelMore.Application.Repositories;
using TravelMore.Domain.Users;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Repositories;

public class UserRepository(TravelMoreContext context) : GenericRepository<User, int>(context), IUserRepository
{
    public async Task<bool> DoesUserExistByEmail(string email)
        => await _context.Users.AnyAsync(user => user.Email == email);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

}
