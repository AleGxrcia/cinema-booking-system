namespace CinemaBookingSystem.Shared.Presentation.Contracts.Models;

public sealed record ErrorDetails(
    string Code,
    string Message,
    string Type,
    int StatusCode,
    IReadOnlyList<ValidationError>? Errors = null
);
