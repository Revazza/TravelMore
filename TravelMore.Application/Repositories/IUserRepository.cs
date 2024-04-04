using TravelMore.Domain.Users;

namespace TravelMore.Application.Repositories;

public interface IUserRepository : IGenericRepository<User, int>
{
    Task<bool> DoesUserExistByEmail(string email);
    Task<User?> GetByEmailAsync(string email);
}
