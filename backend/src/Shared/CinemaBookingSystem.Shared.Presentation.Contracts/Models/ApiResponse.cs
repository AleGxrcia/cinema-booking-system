namespace CinemaBookingSystem.Shared.Presentation.Contracts.Models;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    ResponseMetadata Metadata,
    ErrorDetails? Error = null)
{
    public static ApiResponse<T> Succeed(T data, ResponseMetadata metadata) =>
        new(true, data, metadata, null);

    public static ApiResponse<T> Fail(ErrorDetails error, ResponseMetadata metadata) =>
        new(false, default, metadata, error);
}
