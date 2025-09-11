using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record MovieTitle
{
    public string Value { get; } = default!;
    public string NormalizedValue { get; } = default!;

    protected MovieTitle()
    {
    }

    private MovieTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Movie title cannot be empty");
        }

        if (value.Length > 200)
        {
            throw new DomainException("Movie title cannot exceed 200 characters");
        }

        Value = value.Trim();
        NormalizedValue = NormalizeTitle(value);
    }

    public static MovieTitle Create(string value)
    {
        return new MovieTitle(value);
    }

    private static string NormalizeTitle(string title)
    {
        return string.Concat(title.ToLowerInvariant().Where(char.IsLetterOrDigit));
    }

    public override string ToString()
    {
        return Value;
    }
}
