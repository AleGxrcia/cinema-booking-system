namespace CinemaBookingSystem.Shared.Domain.Common;

public enum ErrorType
{
    None = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3,
    Failure = 4,
    Unauthorized = 5,
    Forbidden = 6,
    Unexpected = 7,
}
