namespace CinemaBookingSystem.Shared.Domain.Abstractions;

public interface IEntity<TId> : IEntity
{
    TId Id { get; }
}

public interface IEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}