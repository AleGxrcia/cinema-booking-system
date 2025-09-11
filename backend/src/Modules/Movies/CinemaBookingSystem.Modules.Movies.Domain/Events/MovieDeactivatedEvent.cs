using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public class MovieDeactivatedEvent(Guid movieId) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
}
