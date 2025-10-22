namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Common;

public sealed record GenreDto(
    Guid Id,
    string Name,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
