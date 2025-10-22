using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieCreatedEvent(
    Guid movieId,
    MovieTitle title,
    string description,
    Duration duration,
    int releaseYear,
    MovieLanguage language,
    string country,
    AgeRating ageRating,
    MovieStatus status,
    DateTime createdAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public MovieTitle Title { get; init; } = title;
    public string Description { get; init; } = description;
    public Duration Duration { get; init; } = duration;
    public int ReleaseYear { get; init; } = releaseYear;
    public MovieLanguage Language { get; init; } = language;
    public string Country { get; init; } = country;
    public AgeRating AgeRating { get; init; } = ageRating;
    public MovieStatus Status { get; init; } = status;
    public DateTime CreatedAt { get; init; } = createdAt;
}
