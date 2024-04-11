using TravelMore.Domain.Guests;

namespace TravelMore.Application.Repositories;

public interface IGuestRepository : IGenericRepository<Guest, int>
{
    Task<bool> DoesGuestExistByEmail(string email);
}
