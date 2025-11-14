using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Repositories;

public class PricingPolicyRepository : IPricingPolicyRepository
{
    private readonly ShowtimesDbContext _dbContext;

    public PricingPolicyRepository(ShowtimesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PricingPolicy policy)
    {
        await _dbContext.PricingPolicies.AddAsync(policy);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PricingPolicy?> GetByIdAsync(Guid id)
    {
        return await _dbContext.PricingPolicies
            .Include(p => p.Rules)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
