using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Repositories;

internal sealed class GenreRepository : IGenreRepository
{
    private readonly MovieDbContext _context;

    public GenreRepository(MovieDbContext context)
    {
        _context = context;
    }

    public void Add(Genre genre)
    {
        _context.Genres.Add(genre);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Genres
            .AsNoTracking()
            .AnyAsync(g =>
                g.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase),
                cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        Guid excludeGenreId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Genres
            .AsNoTracking()
            .AnyAsync(g =>
                g.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) &&
                g.Id != excludeGenreId, cancellationToken);
    }

    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Genres
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Genre>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await _context.Genres
            .AsNoTracking()
            .Where(g => ids.Contains(g.Id))
            .ToListAsync(cancellationToken);
    }

    public void Remove(Genre genre)
    {
        _context.Genres.Remove(genre);
    }
}
