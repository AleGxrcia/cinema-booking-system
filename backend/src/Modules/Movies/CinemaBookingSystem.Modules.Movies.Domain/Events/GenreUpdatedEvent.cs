using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreUpdatedEvent(Guid genreId, string name) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
    public string Name { get; init; } = name;
}
