using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Repositories;

public class HallRepository : IHallRepository
{
    private readonly ShowtimesDbContext _dbContext;

    public HallRepository(ShowtimesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Hall hall)
    {
        await _dbContext.AddAsync(hall);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Hall?> GetByIdAsync(Guid id)
    {
        return await _dbContext.FindAsync<Hall>(id);
    }
}
