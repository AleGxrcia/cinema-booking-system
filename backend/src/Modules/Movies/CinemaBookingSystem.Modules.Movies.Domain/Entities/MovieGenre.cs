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

    public MovieGenre(Guid id, Guid movieId, Guid genreId) : base(id)
    {
        if (movieId == Guid.Empty)
        {
            throw new DomainException("El ID de la película no puede ser vacío");
        }

        if (genreId == Guid.Empty)
        {
            throw new DomainException("El ID del género no puede ser vacío");
        }

        MovieId = movieId;
        GenreId = genreId;
        CreatedAt = DateTime.UtcNow;
    }
}
