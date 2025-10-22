using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieUpdatedEvent(
    Guid movieId,
    MovieTitle title,
    string description,
    Duration duration,
    string? posterUrl,
    string? trailerUrl,
    DateTime updatedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public MovieTitle Title { get; init; } = title;
    public string Description { get; init; } = description;
    public Duration Duration { get; init; } = duration;
    public string? PosterUrl { get; init; } = posterUrl;
    public string? TrailerUrl { get; init; } = trailerUrl;
    public DateTime UpdatedAt { get; init; } = updatedAt;
}
