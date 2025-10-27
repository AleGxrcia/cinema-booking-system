namespace CinemaBookingSystem.Shared.Presentation.Contracts.Models;

public sealed record ResponseMetadata(
    DateTime Timestamp,
    string TraceId,
    string? CorrelationId = null
);
