using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

public class PricingRule : Entity
{
    public SeatType SeatType { get; private set; }
    public HallFormatType HallFormatType { get; private set; }
    public decimal Price { get; private set; }

    private PricingRule() { }

    public static PricingRule Create(SeatType seatType, HallFormatType hallFormatType, decimal price)
    {
        // Add validation logic here if needed
        return new PricingRule
        {
            SeatType = seatType,
            HallFormatType = hallFormatType,
            Price = price
        };
    }
}
