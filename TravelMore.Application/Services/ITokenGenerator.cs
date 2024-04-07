using TravelMore.Domain.Users;

namespace TravelMore.Application.Services;

public interface ITokenGenerator
{
    string Generate(User user);
}
