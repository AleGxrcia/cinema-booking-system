using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieUpdatedEvent : DomainEventBase
{
    public Guid MovieId { get; init; }
    public MovieTitle Title { get; init; }
    public string Description { get; init; }
    public Duration Duration { get; init; }
    public string? PosterUrl { get; init; }
    public string? TrailerUrl { get; init; }

    public MovieUpdatedEvent(Guid movieId, MovieTitle title, string description, Duration duration,
        string posterUrl, string trailerUrl)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        Duration = duration;
        PosterUrl = posterUrl;
        TrailerUrl = trailerUrl;
    }
}
