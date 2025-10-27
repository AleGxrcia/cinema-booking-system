namespace CinemaBookingSystem.Shared.Presentation.Contracts.Models;

public sealed record PagedResponse<T>(
    bool Success,
    IReadOnlyList<T> Data,
    ResponseMetadata Metadata,
    PaginationMetadata? Pagination = null,
    ErrorDetails? Error = null)
{
    public static PagedResponse<T> Succeed(
        IReadOnlyList<T> data,
        int pageNumber,
        int pageSize,
        int totalCount,
        int totalPages,
        bool hasNextPage,
        bool hasPreviousPage,
        ResponseMetadata metadata)
    {
        var pagination = new PaginationMetadata(
            pageNumber,
            pageSize,
            totalCount,
            totalPages,
            hasNextPage,
            hasPreviousPage
        );

        return new(true, data, metadata, pagination, null);
    }

    public static PagedResponse<T> Fail(ErrorDetails error, ResponseMetadata metadata)
        => new(false, Array.Empty<T>(), metadata, null, error);
}

public sealed record PaginationMetadata(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage
);
