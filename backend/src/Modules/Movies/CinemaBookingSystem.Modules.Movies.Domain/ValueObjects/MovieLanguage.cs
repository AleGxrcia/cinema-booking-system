using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record MovieLanguage
{
    private const int MaxNameLength = 50;

    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    protected MovieLanguage()
    {
    }

    private MovieLanguage(string code, string name)
    {
        Code = code.ToLowerInvariant();
        Name = name.Trim();
    }

    public static Result<MovieLanguage> Create(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Error.Validation(
                "MovieLanguage.Code.Empty", 
                "Language code is required");

        if (code.Length != 2 && code.Length != 3)
            return Error.Validation(
                "MovieLanguage.Code.InvalidLength", 
                "Code must be ISO 639-1 (2 chars) or ISO 639-2 (3 chars)");

        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation(
                "MovieLanguage.Name.Empty", 
                "Language name is required");

        if (name.Length > MaxNameLength)
            return Error.Validation(
                "MovieLanguage.Name.TooLong", 
                $"Name cannot exceed {MaxNameLength} characters");


        return new MovieLanguage(code, name);
    }

    public override string ToString() => $"{Name} ({Code})";
}
