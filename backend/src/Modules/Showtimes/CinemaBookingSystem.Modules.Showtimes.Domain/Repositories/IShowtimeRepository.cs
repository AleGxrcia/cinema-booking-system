using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;

public interface IShowtimeRepository
{
    Task AddAsync(Showtime showtime);
    Task<Showtime?> GetByIdAsync(Guid id);
}
