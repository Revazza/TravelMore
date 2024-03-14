using TravelMore.Application.Common.Interfaces.Services;

namespace TravelMore.Infrastructure.Services;

public class UserIdentityService : IUserIdentityService
{
    public int GetUserId()
    {
        return 1;
    }
}
