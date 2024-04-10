namespace TravelMore.Domain.Common.Models;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; init; } = id;
}
