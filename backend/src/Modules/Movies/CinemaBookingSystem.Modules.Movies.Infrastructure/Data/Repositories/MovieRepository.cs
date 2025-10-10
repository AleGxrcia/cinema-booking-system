using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Repositories;

internal sealed class MovieRepository : IMovieRepository
{
    private readonly MovieDbContext _context;

    public MovieRepository(MovieDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
    }

    public async Task<bool> ExistsByTitleAndYearAsync(string normalizedTitle, int releaseYear, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .AsNoTracking()
            .AnyAsync(m =>
                m.Title.NormalizedValue == normalizedTitle &&
                m.ReleaseYear == releaseYear,
                cancellationToken);
    }

    public async Task<bool> ExistsByTitleAndYearAsync(
        string normalizedTitle,
        int releaseYear,
        Guid excludeMovieId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .AsNoTracking()
            .AnyAsync(m =>
                m.Title.NormalizedValue == normalizedTitle &&
                m.ReleaseYear == releaseYear &&
                m.Id != excludeMovieId,
                cancellationToken);
    }

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<Movie?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(m => m.Genres)
                .ThenInclude(mg => mg.Genre)
            .Include(m => m.Cast)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public void Remove(Movie movie)
    {
        _context.Movies.Remove(movie);
    }
}
