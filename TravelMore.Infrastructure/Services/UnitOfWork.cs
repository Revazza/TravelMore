using TravelMore.Application.Common.Interfaces.Services;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Infrastructure.Services;

public class UnitOfWork(TravelMoreContext context) : IUnitOfWork
{
    private readonly TravelMoreContext _context = context;

    public async Task SaveChangesAsync(CancellationToken cancellation = default)
    {
        // TODO: Add logging
        await _context.SaveChangesAsync(cancellation);
    }
}
