using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class MovieGenre : Entity<Guid>
{
    public Guid MovieId { get; private set; }
    public Guid GenreId { get; private set; }
    public Movie Movie { get; private set; } = default!;
    public Genre Genre { get; private set; } = default!;

    private MovieGenre()
    {
    }

    private MovieGenre(Guid id, Guid movieId, Guid genreId) : base(id)
    {
        MovieId = movieId;
        GenreId = genreId;
        CreatedAt = DateTime.UtcNow;
    }

    public static Result<MovieGenre> Create(Guid id, Guid movieId, Guid genreId)
    {
        if (movieId == Guid.Empty)
            return MovieGenreErrors.MovieIdEmpty();

        if (genreId == Guid.Empty)
            return MovieGenreErrors.GenreIdEmpty();

        return new MovieGenre(id, movieId, genreId);
    }
}
