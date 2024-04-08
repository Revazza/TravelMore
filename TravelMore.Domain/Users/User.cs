using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Users;

public abstract class User : Entity<int>
{
    private User() : base(default)
    {

    }

    protected User(int id, string email, string passwordHash, string salt) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
    }

    public string Email { get; protected set; } = string.Empty;

    public string PasswordHash { get; protected set; } = string.Empty;

    public string Salt { get; protected set; } = string.Empty;

}