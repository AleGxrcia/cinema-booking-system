using CinemaBookingSystem.Modules.Movies.Domain.Entities;

namespace CinemaBookingSystem.Modules.Movies.Domain.Repositories;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Genre>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Genre genre, CancellationToken cancellationToken = default);
    Task UpdateAsync(Genre genre, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}