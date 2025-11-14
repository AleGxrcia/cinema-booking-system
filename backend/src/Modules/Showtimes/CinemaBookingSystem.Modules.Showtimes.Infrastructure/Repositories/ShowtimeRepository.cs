using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Repositories;

public class ShowtimeRepository : IShowtimeRepository
{
    private readonly ShowtimesDbContext _dbContext;

    public ShowtimeRepository(ShowtimesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Showtime showtime)
    {
        await _dbContext.Showtimes.AddAsync(showtime);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Showtime?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Showtimes.FindAsync(id);
    }
}
