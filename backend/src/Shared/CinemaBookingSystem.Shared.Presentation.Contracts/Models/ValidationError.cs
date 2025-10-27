namespace CinemaBookingSystem.Shared.Presentation.Contracts.Models;

public sealed record ValidationError(
    string Field,
    string Code,
    string Message
);
