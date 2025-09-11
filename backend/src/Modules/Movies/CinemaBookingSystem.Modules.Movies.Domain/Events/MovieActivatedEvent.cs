using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieActivatedEvent(Guid movieId) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
}
