using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Modules.Movies.Domain.Events;
using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class Movie : AggregateRoot<Guid>
{
    private readonly List<MovieGenre> _genres = [];
    private readonly List<MovieCast> _cast = [];

    public MovieTitle Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public Duration Duration { get; private set; } = default!;
    public int ReleaseYear { get; private set; } = default!;
    public string? PosterUrl { get; private set; }
    public string? TrailerUrl { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public MovieStatus Status { get; private set; }
    public MovieLanguage Language { get; private set; } = default!;
    public string Country { get; private set; } = default!;

    public IReadOnlyCollection<MovieGenre> Genres => _genres.AsReadOnly();
    public IReadOnlyCollection<MovieCast> Cast => _cast.AsReadOnly();

    private Movie()
    {
    }

    public Movie(Guid id, MovieTitle title, string description, Duration duration,
        int releaseYear, MovieLanguage language, string country, AgeRating ageRating = AgeRating.G) : base(id)
    {
        Title = title;
        Description = ValidateDescription(description);
        Duration = duration;
        ReleaseYear = ValidateReleaseYear(releaseYear);
        Language = language;
        Country = ValidateCountry(country);
        AgeRating = ageRating;
        Status = MovieStatus.Active;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new MovieCreatedEvent(Id, Title, Duration, ReleaseYear, Language, Country));
    }

    public void UpdateInformation(MovieTitle title, string description, Duration duration,
        string posterUrl, string trailerUrl)
    {
        if (Status != MovieStatus.Active)
        {
            throw new DomainException("No se puede actualizar una película inactiva");
        }

        Title = title;
        Description = ValidateDescription(description);
        Duration = duration;
        PosterUrl = posterUrl;
        TrailerUrl = trailerUrl;
        MarkAsUpdated();

        AddDomainEvent(new MovieUpdatedEvent(Id, Title, Description, Duration, posterUrl, trailerUrl));
    }

    public void AddGenre(Genre genre)
    {
        if (genre == null)
        {
            throw new ArgumentNullException(nameof(genre));
        }

        if (!genre.IsActive)
        {
            throw new DomainException("No se puede asignar un género inactivo");
        }

        if (_genres.Any(g => g.GenreId == genre.Id))
        {
            throw new DomainException("La película ya tiene asignado este género");
        }

        var movieGenre = new MovieGenre(Guid.NewGuid(), Id, genre.Id);
        _genres.Add(movieGenre);

        AddDomainEvent(new MovieGenreAddedEvent(Id, genre.Id));
    }

    public void RemoveGenre(Guid genreId)
    {
        var movieGenre = _genres.FirstOrDefault(g => g.GenreId == genreId);
        if (movieGenre == null)
        {
            throw new DomainException("La película no tiene asignado este género");
        }

        _genres.Remove(movieGenre);

        AddDomainEvent(new MovieGenreRemovedEvent(Id, genreId));
    }

    public void AddCastMember(string personName, CastRole role)
    {
        if (string.IsNullOrWhiteSpace(personName))
        {
            throw new DomainException("El nombre de la persona es obligatorio");
        }

        var castMember = new MovieCast(Guid.NewGuid(), Id, personName, role);
        _cast.Add(castMember);

        AddDomainEvent(new MovieCastAddedEvent(Id, castMember.Id, personName, role));
    }

    public void RemoveCastMember(Guid castId)
    {
        var castMember = _cast.FirstOrDefault(c => c.Id == castId);
        if (castMember == null)
        {
            throw new DomainException("No se encontró el miembro del reparto");
        }

        _cast.Remove(castMember);

        AddDomainEvent(new MovieCastRemovedEvent(Id, castId, castMember.PersonName));
    }

    public void Deactivate()
    {
        if (Status == MovieStatus.Inactive)
        {
            throw new DomainException("La película ya está inactiva");
        }

        Status = MovieStatus.Inactive;
        MarkAsUpdated();

        AddDomainEvent(new MovieDeactivatedEvent(Id));
    }

    public void Activate()
    {
        if (Status == MovieStatus.Active)
        {
            throw new DomainException("La película ya está activa");
        }

        Status = MovieStatus.Active;
        MarkAsUpdated();

        AddDomainEvent(new MovieActivatedEvent(Id));
    }

    public void ChangeStatus(MovieStatus newStatus)
    {
        if (Status == newStatus)
        {
            return;
        }

        var previousStatus = Status;
        Status = newStatus;
        MarkAsUpdated();

        AddDomainEvent(new MovieStatusChangedEvent(Id, previousStatus, newStatus));
    }

    private static string ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException("La descripción es obligatoria");
        }

        if (description.Length > 1000)
        {
            throw new DomainException("La descripción no puede exceder 1000 caracteres");
        }

        return description.Trim();
    }

    private static int ValidateReleaseYear(int releaseYear)
    {
        var currentYear = DateTime.Now.Year;
        var minYear = 1888;

        if (releaseYear < minYear)
        {
            throw new DomainException($"El año de lanzamiento no puede ser anterior a {minYear}");
        }

        if (releaseYear > currentYear + 5)
        {
            throw new DomainException("El año de lanzamiento no puede ser más de 5 años en el futuro");
        }

        return releaseYear;
    }

    private static string ValidateCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new DomainException("El país de origen no puede estar vacío");
        }

        if (country.Length < 2)
        {
            throw new DomainException("El nombre del país debe tener al menos 2 caracteres");
        }

        if (country.Length > 100)
        {
            throw new DomainException("El nombre del país no puede tener más de 100 caracteres");
        }

        return country.Trim();
    }
}
