using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieCreatedEvent : DomainEventBase
{
    public Guid MovieId { get; init; }
    public MovieTitle Title { get; init; }
    public Duration Duration { get; init; }
    public int ReleaseYear { get; init; }
    public MovieLanguage Language { get; init; }
    public string Country { get; init; }

    public MovieCreatedEvent(Guid movieId, MovieTitle title, Duration duration, int releaseYear,
        MovieLanguage language, string country)
    {
        MovieId = movieId;
        Title = title;
        Duration = duration;
        ReleaseYear = releaseYear;
        Language = language;
        Country = country;
    }
}
