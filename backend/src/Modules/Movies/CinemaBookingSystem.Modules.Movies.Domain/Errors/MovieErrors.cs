using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Errors;

public static class MovieErrors
{
    // Description errors
    public static Error DescriptionEmpty() =>
        Error.Validation("Movie.Description.Empty", "Description is required");

    public static Error DescriptionTooLong(int maxLength) =>
        Error.Validation("Movie.Description.TooLong", 
            $"Description cannot exceed {maxLength} characters");

    // Release Year errors
    public static Error ReleaseYearTooOld(int minYear) =>
        Error.Validation("Movie.ReleaseYear.TooOld", 
            $"Release year cannot be earlier than {minYear}");

    public static Error ReleaseYearTooFuture(int maxYearsAhead) =>
        Error.Validation("Movie.ReleaseYear.TooFuture", 
            $"Release year cannot be more than {maxYearsAhead} years in the future");

    // Country errors
    public static Error CountryEmpty() =>
        Error.Validation("Movie.Country.Empty", "Country of origin is required");

    public static Error CountryTooShort(int minLength) =>
        Error.Validation("Movie.Country.TooShort", 
            $"Country name must have at least {minLength} characters");

    public static Error CountryTooLong(int maxLength) =>
        Error.Validation("Movie.Country.TooLong", 
            $"Country name cannot exceed {maxLength} characters");

    // Status
    public static Error AlreadyInactive() =>
        Error.Conflict("Movie.Status.AlreadyInactive", "Movie is already inactive");

    public static Error AlreadyActive() =>
        Error.Conflict("Movie.Status.AlreadyActive", "Movie is already active");

    public static Error CannotUpdateInactive() =>
        Error.Conflict("Movie.Update.Inactive", "Cannot update an inactive movie");

    // Genre relationships
    public static Error GenreNull() =>
        Error.Validation("Movie.Genre.Null", "Genre cannot be null");

    public static Error GenreInactive() =>
        Error.Validation("Movie.Genre.Inactive", "Cannot assign an inactive genre");

    public static Error GenreAlreadyAssigned() =>
        Error.Conflict("Movie.Genre.AlreadyAssigned", "Movie already has this genre assigned");

    public static Error GenreNotAssigned() =>
        Error.NotFound("Movie.Genre.NotAssigned", "Movie does not have this genre assigned");

    // Cast relationships
    public static Error CastMemberNotFound() =>
        Error.NotFound("Movie.Cast.NotFound", "Cast member not found");

    // Query errors
    public static Error NotFound(Guid movieId) =>
        Error.NotFound("Movie.NotFound", $"Movie with ID '{movieId}' was not found");
}