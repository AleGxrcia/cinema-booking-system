using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record MovieTitle
{
    private const int MaxLength = 200;

    public string Value { get; } = default!;
    public string NormalizedValue { get; } = default!;

    protected MovieTitle()
    {
    }

    private MovieTitle(string value)
    {
        Value = value.Trim();
        NormalizedValue = NormalizeTitle(value);
    }

    public static Result<MovieTitle> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Error.Validation(
                "MovieTitle.Empty", "Movie title cannot be empty");

        if (value.Length > MaxLength)
            return Error.Validation(
                "MovieTitle.TooLong", $"Movie title cannot exceed {MaxLength} characters");

        return new MovieTitle(value);
    }

    private static string NormalizeTitle(string title)
    {
        return string.Concat(title.ToLowerInvariant().Where(char.IsLetterOrDigit));
    }

    public override string ToString() => Value;
}
