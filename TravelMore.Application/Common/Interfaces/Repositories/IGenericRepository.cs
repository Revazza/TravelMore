
namespace TravelMore.Application.Common.Interfaces.Repositories;

public interface IGenericRepository<T, TId>
    where T : class
{
    Task<T?> AddAsync(T entity);
    IQueryable<T> AsQuery();
    Task<T?> GetByIdAsync(TId id);
    void Update(T entity);
    void UpdateRange(List<T> entities);
    void Delete(T entity);
}
