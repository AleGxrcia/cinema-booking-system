using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieGenreRemovedEvent(Guid movieId, Guid genreId, string genreName, DateTime removedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid GenreId { get; init; } = genreId;
    public string GenreName { get; init; } = genreName;
    public DateTime RemovedAt { get; init; } = removedAt;
}
