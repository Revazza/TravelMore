namespace TravelMore.Domain.Shared.Models;

public abstract class Entity<T>(T id)
{
    public T Id { get; init; } = id;
}
