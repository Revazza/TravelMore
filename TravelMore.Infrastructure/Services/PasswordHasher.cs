using TravelMore.Application.Services.PasswordHasher;

namespace TravelMore.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public PasswordHasherResponse Hash(string password) => GenerateHashedPasswordWithSalt(password, GenerateSalt());

    public PasswordHasherResponse Hash(string password, string salt) => GenerateHashedPasswordWithSalt(password, salt);

    private static PasswordHasherResponse GenerateHashedPasswordWithSalt(string password, string salt)
        => new(GenerateHashedPassword(password + salt), salt);

    private static string GenerateSalt() => BCrypt.Net.BCrypt.GenerateSalt();

    private static string GenerateHashedPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

}
