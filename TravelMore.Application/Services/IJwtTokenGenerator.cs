using TravelMore.Domain.Users;

namespace TravelMore.Application.Services;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
