using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Users;

public class User(int id, string email, string passwordHash, string salt) : Entity<int>(id)
{
    public string Email { get; set; } = email;
    public string PasswordHash { get; set; } = passwordHash;
    public string Salt { get; set; } = salt;

}
