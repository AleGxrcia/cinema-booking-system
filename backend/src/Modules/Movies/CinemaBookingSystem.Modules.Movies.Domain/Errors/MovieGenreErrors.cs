using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Errors;

public static class MovieGenreErrors
{
    public static Error MovieIdEmpty() =>
        Error.Validation("MovieGenre.MovieId.Empty", "Movie ID cannot be empty");

    public static Error GenreIdEmpty() =>
        Error.Validation("MovieGenre.GenreId.Empty", "Genre ID cannot be empty");
}