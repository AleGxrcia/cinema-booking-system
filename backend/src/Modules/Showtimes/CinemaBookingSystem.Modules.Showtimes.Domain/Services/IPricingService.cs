using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Services;

public interface IPricingService
{
    Task<Result<decimal>> CalculatePrice(Showtime showtime, SeatType seatType);
}
