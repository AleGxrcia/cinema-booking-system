using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Errors;

public static class GenreErrors
{
    public static Error NameEmpty() =>
        Error.Validation("Genre.Name.Empty", "Genre name cannot be empty");

    public static Error NameTooShort(int minLength) =>
        Error.Validation("Genre.Name.TooShort", 
            $"Genre name must have at least {minLength} characters");

    public static Error NameTooLong(int maxLength) =>
        Error.Validation("Genre.Name.TooLong", 
            $"Genre name cannot exceed {maxLength} characters");

    public static Error AlreadyActive() =>
        Error.Conflict("Genre.Status.AlreadyActive", "Genre is already active");

    public static Error AlreadyInactive() =>
        Error.Conflict("Genre.Status.AlreadyInactive", "Genre is already inactive");

    public static Error CannotUpdateInactive() =>
        Error.Conflict("Genre.Update.Inactive", "Cannot update an inactive genre");

    public static Error NotFound(Guid genreId) =>
        Error.NotFound("Genre.NotFound", $"Genre with ID '{genreId}' was not found");
}