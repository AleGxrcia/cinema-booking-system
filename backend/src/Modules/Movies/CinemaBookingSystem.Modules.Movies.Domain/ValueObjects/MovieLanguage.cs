using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;

public record MovieLanguage
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    protected MovieLanguage()
    {
    }

    private MovieLanguage(string code, string name)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new DomainException("El código de idioma es obligatorio");
        }

        if (code.Length != 2 && code.Length != 3)
        {
            throw new DomainException("El código debe ser ISO 639-1 (2 chars) o ISO 639-2 (3 chars)");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("El nombre de idioma es obligatorio");
        }

        if (name.Length > 50)
        {
            throw new DomainException("El nombre no puede exceder 50 caracteres");
        }

        Code = code.ToLowerInvariant();
        Name = name.Trim();
    }

    public static MovieLanguage Create(string code, string name)
    {
        return new MovieLanguage(code, name);
    }

    public override string ToString()
    {
        return $"{Name} ({Code})";
    }
}
