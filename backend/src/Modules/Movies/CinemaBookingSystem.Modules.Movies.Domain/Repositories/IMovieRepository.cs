using CinemaBookingSystem.Modules.Movies.Domain.Entities;

namespace CinemaBookingSystem.Modules.Movies.Domain.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Movie?> GetByIdWithDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByTitleAndYearAsync(
        string normalizedTitle,
        int releaseYear,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByTitleAndYearAsync(
        string normalizedTitle,
        int releaseYear,
        Guid excludeMovieId,
        CancellationToken cancellationToken = default);

    void Add(Movie movie);
    void Remove(Movie movie);
}