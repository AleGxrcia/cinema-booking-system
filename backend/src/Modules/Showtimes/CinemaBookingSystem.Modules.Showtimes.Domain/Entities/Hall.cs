using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Domain.Entities;

public class Hall : AggregateRoot
{
    public string Name { get; private set; }
    public HallFormatType Format { get; private set; }

    private Hall() { }

    public static Hall Create(string name, HallFormatType format)
    {
        return new Hall
        {
            Name = name,
            Format = format
        };
    }
}
