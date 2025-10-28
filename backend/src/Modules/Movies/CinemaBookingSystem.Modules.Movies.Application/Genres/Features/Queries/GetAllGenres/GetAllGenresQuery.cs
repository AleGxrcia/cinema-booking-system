using CinemaBookingSystem.Modules.Movies.Application.Genres.Dtos;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Queries.GetAllGenres;

public sealed record GetAllGenresQuery(
    int PageNumber = PagedRequest.MinPageNumber,
    int PageSize = PagedRequest.DefaultPageSize) : PagedRequest, IQuery<PagedResult<GenreDto>>
{
    public new int PageNumber { get; init; } = PageNumber;
    public new int PageSize { get; init; } = PageSize;
}