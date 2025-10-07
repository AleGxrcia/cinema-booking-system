using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Shared.Application.Common;

public sealed class PagedResult<TValue> : Result<IReadOnlyList<TValue>>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;

    private PagedResult(IReadOnlyList<TValue> items,
        int pageNumber,
        int pageSize,
        int totalCount) : base(items, true, Error.None)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = pageSize > 0
            ? (int)Math.Ceiling((double)totalCount / pageSize)
            : 0;
    }

    private PagedResult(Error error) : base(default, false, error)
    {
        PageNumber = 0;
        PageSize = 0;
        TotalCount = 0;
        TotalPages = 0;
    }

    public static PagedResult<TValue> Success(
        IReadOnlyList<TValue> items,
        int pageNumber,
        int pageSize,
        int totalCount) => new(items, pageNumber, pageSize, totalCount);

    public static PagedResult<TValue> Empty(int pageNumber, int pageSize) =>
        new(Array.Empty<TValue>(), pageNumber, pageSize, 0);

    public static new PagedResult<TValue> Failure(Error error) => new(error);

    public static implicit operator PagedResult<TValue>(Error error) => Failure(error);
}
