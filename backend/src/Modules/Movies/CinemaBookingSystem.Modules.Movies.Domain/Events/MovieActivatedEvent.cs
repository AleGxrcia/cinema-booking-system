using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieActivatedEvent(Guid movieId, DateTime activatedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public DateTime ActivatedAt { get; init; } = activatedAt;
}