using CinemaBookingSystem.Modules.Movies.Domain.Enums;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;

public record MovieDto(
    Guid Id,
    string Title,
    string Description,
    int DurationInMinutes,
    string DurationFormatted,
    int ReleaseYear,
    string? PosterUrl,
    string? TrailerUrl,
    AgeRating AgeRating,
    MovieStatus Status,
    string LanguageCode,
    string LanguageName,
    string Country,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
