using Microsoft.EntityFrameworkCore;
using TravelMore.Persistance.Contexts.TravelMore;
using TravelMore.Application.Repositories;

namespace TravelMore.Infrastructure.Repositories;

public class GenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : class
{
    protected readonly TravelMoreContext _context;
    private readonly DbSet<T> _entities;

    public GenericRepository(TravelMoreContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }

    public async Task<T?> AddAsync(T entity)
    {
        return (await _entities.AddAsync(entity)).Entity;
    }

    public virtual void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public IQueryable<T> AsQuery()
    {
        return _entities.AsQueryable();
    }

    public virtual async Task<T?> GetByIdAsync(TId id)
    {
        return await _entities.FindAsync(id);
    }

    public virtual void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(List<T> entities)
    {
        _context.UpdateRange(entities);
    }

}
