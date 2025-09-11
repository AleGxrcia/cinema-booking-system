using CinemaBookingSystem.Modules.Movies.Domain.Entities;

namespace CinemaBookingSystem.Modules.Movies.Domain.Repositories;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Movie>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Movie>> GetActiveMoviesAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Movie movie, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movie movie, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}