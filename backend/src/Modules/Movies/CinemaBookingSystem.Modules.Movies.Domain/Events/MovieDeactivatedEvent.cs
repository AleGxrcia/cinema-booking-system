using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieDeactivatedEvent(Guid movieId, DateTime deactivatedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public DateTime DeactivatedAt { get; init; } = deactivatedAt;
}
