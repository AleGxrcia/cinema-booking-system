using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieGenreRemovedEvent(Guid movieId, Guid genreId) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid GenreId { get; init; } = genreId;
}
