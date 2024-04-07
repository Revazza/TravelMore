using TravelMore.Domain.Users.StandartGuests;

namespace TravelMore.Application.Repositories;

public interface IStandardGuestRepository : IGenericRepository<StandardGuest, int>
{
    Task<StandardGuest?> GetByEmailAsync(string email);
}
