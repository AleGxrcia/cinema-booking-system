using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Events;
using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class Movie : AggregateRoot<Guid>
{
    private const int MaxDescriptionLength = 1000;
    private const int MinReleaseYear = 1888;
    private const int MaxYearsAhead = 5;
    private const int MinCountryLength = 2;
    private const int MaxCountryLength = 100;

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

    private Movie(Guid id, MovieTitle title, string description, Duration duration,
        int releaseYear, MovieLanguage language, string country, AgeRating ageRating) : base(id)
    {
        Title = title;
        Description = description;
        Duration = duration;
        ReleaseYear = releaseYear;
        Language = language;
        Country = country;
        AgeRating = ageRating;
        Status = MovieStatus.Active;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new MovieCreatedEvent(Id, Title, Duration, ReleaseYear, Language, Country));
    }

        public static Result<Movie> Create(Guid id, MovieTitle title, string description, Duration duration,
            int releaseYear, MovieLanguage language, string country, AgeRating ageRating = AgeRating.G)
    {
        var descriptionResult = ValidateDescription(description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        var releaseYearResult = ValidateReleaseYear(releaseYear);
        if (releaseYearResult.IsFailure)
            return releaseYearResult.Error;

        var countryResult = ValidateCountry(country);
        if (countryResult.IsFailure)
            return countryResult.Error;

        return new Movie(
            id,
            title,
            descriptionResult.Value,
            duration,
            releaseYearResult.Value,
            language,
            countryResult.Value,
            ageRating
        );
    }

    public Result UpdateInformation(MovieTitle title, string description, Duration duration,
        string? posterUrl, string? trailerUrl)
    {
        if (Status != MovieStatus.Active)
            return MovieErrors.CannotUpdateInactive();

        var descriptionResult = ValidateDescription(description);
        if (descriptionResult.IsFailure)
            return descriptionResult.Error;

        Title = title;
        Description = descriptionResult.Value;
        Duration = duration;
        PosterUrl = posterUrl;
        TrailerUrl = trailerUrl;
        MarkAsUpdated();

        AddDomainEvent(new MovieUpdatedEvent(Id, Title, Description, Duration,
            posterUrl ?? string.Empty, trailerUrl ?? string.Empty));

        return Result.Success();
    }

    public Result AddGenre(Genre genre)
    {
        if (genre == null)
            return MovieErrors.GenreNull();

        if (!genre.IsActive)
            return MovieErrors.GenreInactive();

        if (_genres.Any(g => g.GenreId == genre.Id))
            return MovieErrors.GenreAlreadyAssigned();

        var movieGenreResult = MovieGenre.Create(Guid.NewGuid(), Id, genre.Id);
        if (movieGenreResult.IsFailure)
            return movieGenreResult.Error;

        _genres.Add(movieGenreResult.Value);
        AddDomainEvent(new MovieGenreAddedEvent(Id, genre.Id));

        return Result.Success();
    }

    public Result RemoveGenre(Guid genreId)
    {
        var movieGenre = _genres.FirstOrDefault(g => g.GenreId == genreId);
        if (movieGenre == null)
            return MovieErrors.GenreNotAssigned();

        _genres.Remove(movieGenre);
        AddDomainEvent(new MovieGenreRemovedEvent(Id, genreId));

        return Result.Success();
    }

    public Result AddCastMember(string personName, CastRole role)
    {
        var castResult = MovieCast.Create(Guid.NewGuid(), Id, personName, role);
        if (castResult.IsFailure)
            return castResult.Error;

        _cast.Add(castResult.Value);
        AddDomainEvent(new MovieCastAddedEvent(Id, castResult.Value.Id, personName, role));

        return Result.Success();
    }

    public Result RemoveCastMember(Guid castId)
    {
        var castMember = _cast.FirstOrDefault(c => c.Id == castId);
        if (castMember == null)
            return MovieErrors.CastMemberNotFound();

        _cast.Remove(castMember);
        AddDomainEvent(new MovieCastRemovedEvent(Id, castId, castMember.PersonName));

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (Status == MovieStatus.Inactive)
            return MovieErrors.AlreadyInactive();

        Status = MovieStatus.Inactive;
        MarkAsUpdated();
        AddDomainEvent(new MovieDeactivatedEvent(Id));

        return Result.Success();
    }

    public Result Activate()
    {
        if (Status == MovieStatus.Active)
            return MovieErrors.AlreadyActive();

        Status = MovieStatus.Active;
        MarkAsUpdated();
        AddDomainEvent(new MovieActivatedEvent(Id));

        return Result.Success();
    }

    public Result ChangeStatus(MovieStatus newStatus)
    {
        if (Status == newStatus)
            return Result.Success();

        var previousStatus = Status;
        Status = newStatus;
        MarkAsUpdated();
        AddDomainEvent(new MovieStatusChangedEvent(Id, previousStatus, newStatus));

        return Result.Success();
    }

    private static Result<string> ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return MovieErrors.DescriptionEmpty();

        if (description.Length > MaxDescriptionLength)
            return MovieErrors.DescriptionTooLong(MaxDescriptionLength);

        return description.Trim();
    }

    private static Result<int> ValidateReleaseYear(int releaseYear)
    {
        var currentYear = DateTime.Now.Year;

        if (releaseYear < MinReleaseYear)
            return MovieErrors.ReleaseYearTooOld(MinReleaseYear);

        if (releaseYear > currentYear + MaxYearsAhead)
            return MovieErrors.ReleaseYearTooFuture(MaxYearsAhead);

        return releaseYear;
    }

    private static Result<string> ValidateCountry(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return MovieErrors.CountryEmpty();

        if (country.Length < MinCountryLength)
            return MovieErrors.CountryTooShort(MinCountryLength);

        if (country.Length > MaxCountryLength)
            return MovieErrors.CountryTooLong(MaxCountryLength);

        return country.Trim();
    }
}
