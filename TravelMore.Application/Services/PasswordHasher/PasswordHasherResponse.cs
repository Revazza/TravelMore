namespace TravelMore.Application.Services.PasswordHasher;

public record PasswordHasherResponse(string HashedPassword, string Salt);
