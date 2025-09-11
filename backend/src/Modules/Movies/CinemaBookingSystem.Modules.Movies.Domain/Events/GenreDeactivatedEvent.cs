using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreDeactivatedEvent(Guid genreId) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
}
