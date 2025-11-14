using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;

public interface IHallRepository
{
    Task AddAsync(Hall hall);
    Task<Hall?> GetByIdAsync(Guid id);
}
