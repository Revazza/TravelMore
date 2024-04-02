namespace TravelMore.Application.Services;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellation = default);
}
