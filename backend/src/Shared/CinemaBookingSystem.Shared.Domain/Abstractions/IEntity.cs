namespace CinemaBookingSystem.Shared.Domain.Abstractions;

public interface IEntity<TId>
{
    TId Id { get; }
    DateTime? CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}