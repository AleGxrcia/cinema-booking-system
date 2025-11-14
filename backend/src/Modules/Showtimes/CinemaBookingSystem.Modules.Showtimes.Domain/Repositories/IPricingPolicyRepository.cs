using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;

public interface IPricingPolicyRepository
{
    Task AddAsync(PricingPolicy policy);
    Task<PricingPolicy?> GetByIdAsync(Guid id);
}
