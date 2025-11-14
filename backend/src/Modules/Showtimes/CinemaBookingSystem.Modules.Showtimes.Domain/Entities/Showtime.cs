using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

public class Showtime : AggregateRoot
{
    public Guid MovieId { get; private set; }
    public Guid HallId { get; private set; }
    public Guid PricingPolicyId { get; private set; }
    public DateTime StartTime { get; private set; }

    private Showtime() { }

    public static Showtime Create(Guid movieId, Guid hallId, Guid pricingPolicyId, DateTime startTime)
    {
        return new Showtime
        {
            MovieId = movieId,
            HallId = hallId,
            PricingPolicyId = pricingPolicyId,
            StartTime = startTime
        };
    }
}
