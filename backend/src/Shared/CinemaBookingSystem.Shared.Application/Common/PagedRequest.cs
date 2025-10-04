namespace CinemaBookingSystem.Shared.Application.Common;

public record PagedRequest()
{
    public const int MaxPageSize = 100;
    public const int DefaultPageSize = 10;
    public const int MinPageNumber = 1;

    private int _pageNumber = MinPageNumber;
    private int _pageSize = DefaultPageSize;

    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value < MinPageNumber ? MinPageNumber : value;
    }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}
