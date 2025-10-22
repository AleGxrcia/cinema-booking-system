using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieGenreAddedEvent(Guid movieId, Guid genreId, string genreName, DateTime addedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid GenreId { get; init; } = genreId;
    public string GenreName { get; init; } = genreName;
    public DateTime AddedAt { get; init; } = addedAt;
}
