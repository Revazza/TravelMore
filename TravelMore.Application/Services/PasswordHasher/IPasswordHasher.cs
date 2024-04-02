namespace TravelMore.Application.Services.PasswordHasher;

public interface IPasswordHasher
{
    PasswordHasherResponse Hash(string password);
}
