using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record Duration
{
    private const int MaxMinutes = 600;

    public int TotalMinutes { get; }

    private Duration(int totalMinutes)
    {
        TotalMinutes = totalMinutes;
    }

    public static Result<Duration> FromMinutes(int minutes)
    {
        if (minutes <= 0)
            return Error.Validation(
                "Duration.Invalid",
                "Duration must be greater than 0 minutes");

        if (minutes > MaxMinutes)
            return Error.Validation(
                "Duration.TooLong",
                $"Duration cannot exceed {MaxMinutes} minutes (10 hours)");

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

    public override string ToString() => ToFormattedString();
}
