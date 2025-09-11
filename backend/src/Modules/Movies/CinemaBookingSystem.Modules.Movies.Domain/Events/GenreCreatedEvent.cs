using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreCreatedEvent(Guid genreId, string genreName) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
    public string GenreName { get; init; } = genreName;
}
