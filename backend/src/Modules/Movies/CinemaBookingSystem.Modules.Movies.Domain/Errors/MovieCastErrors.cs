using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Errors;

public static class MovieCastErrors
{
    public static Error MovieIdEmpty() =>
        Error.Validation("MovieCast.MovieId.Empty", "Movie ID is required");

    public static Error PersonNameEmpty() =>
        Error.Validation("MovieCast.PersonName.Empty", "Person name cannot be empty");

    public static Error PersonNameTooShort(int minLength) =>
        Error.Validation("MovieCast.PersonName.TooShort", 
            $"Person name must have at least {minLength} characters");

    public static Error PersonNameTooLong(int maxLength) =>
        Error.Validation("MovieCast.PersonName.TooLong", 
            $"Person name cannot exceed {maxLength} characters");

    public static Error OrderNegative() =>
        Error.Validation("MovieCast.Order.Negative", "Order cannot be negative");
}
