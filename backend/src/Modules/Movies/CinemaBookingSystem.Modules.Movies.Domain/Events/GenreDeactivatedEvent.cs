using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreDeactivatedEvent(Guid genreId, DateTime deactivatedAt) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
    public DateTime DeactivatedAt { get; init; } = deactivatedAt;
}
