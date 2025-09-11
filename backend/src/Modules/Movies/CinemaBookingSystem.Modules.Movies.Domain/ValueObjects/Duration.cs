using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record Duration
{
    public int TotalMinutes { get; }

    private Duration(int totalMinutes)
    {
        if (totalMinutes <= 0)
        {
            throw new DomainException("La duración debe ser mayor a 0 minutos");
        }

        if (totalMinutes > 600)
        {
            throw new DomainException("La duración no puede ser mayor a 600 minutos (10 horas)");
        }

        TotalMinutes = totalMinutes;
    }

    public static Duration FromMinutes(int minutes)
    {
        return new Duration(minutes);
    }

    public static Duration FromTimeSpan(TimeSpan timeSpan)
    {
        return new Duration((int)timeSpan.TotalMinutes);
    }

    public string ToFormattedString()
    {
        var hours = TotalMinutes / 60;
        var minutes = TotalMinutes % 60;

        if (hours == 0) return $"{minutes}min";

        if (minutes == 0) return $"{hours}h";

        return $"{hours}h {minutes}min";
    }

    public override string ToString()
    {
        return ToFormattedString();
    }
}
