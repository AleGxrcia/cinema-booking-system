using CinemaBookingSystem.Modules.Movies.Domain.Enums;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMovieDetails;

public sealed record MovieDetailsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int DurationInMinutes { get; init; }
    public string DurationFormatted { get; init; } = string.Empty;
    public int ReleaseYear { get; init; }
    public string? PosterUrl { get; init; }
    public string? TrailerUrl { get; init; }
    public AgeRating AgeRating { get; init; }
    public MovieStatus Status { get; init; }
    public string LanguageCode { get; init; } = string.Empty;
    public string LanguageName { get; init; } = string.Empty;
    public string Country { get; init; } = string.Empty;
    public List<MovieGenreDto> Genres { get; init; } = [];
    public List<MovieCastDto> Cast { get; init; } = [];
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public sealed record MovieGenreDto
{
    public Guid GenreId { get; init; }
    public string GenreName { get; init; } = string.Empty;
}

public sealed record MovieCastDto
{
    public Guid CastId { get; init; }
    public string PersonName { get; init; } = string.Empty;
    public CastRole Role { get; init; }
    public int Order { get; init; }
}
