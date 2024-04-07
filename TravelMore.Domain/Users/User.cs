using TravelMore.Domain.Common.Models;

namespace TravelMore.Domain.Users;

public class User : Entity<int>
{
    private User() : base(default)
    {

    }

    protected User(int id, string email, string passwordHash, string salt, string type) : base(id)
    {
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Type = type;
    }

    public string Email { get; protected set; } = string.Empty;

    public string PasswordHash { get; protected set; } = string.Empty;

    public string Salt { get; protected set; } = string.Empty;

    public string Type { get; protected set; } = nameof(User);
}