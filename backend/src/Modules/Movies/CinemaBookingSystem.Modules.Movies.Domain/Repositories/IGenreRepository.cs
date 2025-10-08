using CinemaBookingSystem.Modules.Movies.Domain.Entities;

namespace CinemaBookingSystem.Modules.Movies.Domain.Repositories;

public interface IGenreRepository
{
    Task<Genre?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Genre>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        string name,
        Guid excludeGenreId,
        CancellationToken cancellationToken = default);

    void Add(Genre genre);
    void Remove(Genre genre);
}