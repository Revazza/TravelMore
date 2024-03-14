namespace TravelMore.Application.Common.Interfaces.Services;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellation = default);
}
