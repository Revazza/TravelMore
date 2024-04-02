using TravelMore.Application.Services.PasswordHasher;

namespace TravelMore.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public PasswordHasherResponse Hash(string password)
    {
        // Very creative I know
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var passwordWithSalt = password + salt;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordWithSalt);

        return new(hashedPassword, salt);
    }
}
