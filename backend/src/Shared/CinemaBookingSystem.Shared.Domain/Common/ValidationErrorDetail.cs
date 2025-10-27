namespace CinemaBookingSystem.Shared.Domain.Common;

public sealed record ValidationErrorDetail(
    string PropertyName,
    string ErrorCode,
    string ErrorMessage
);
