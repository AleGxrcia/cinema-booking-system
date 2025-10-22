using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetAllMovies;

public sealed record GetAllMoviesQuery(
    int PageNumber = PagedRequest.MinPageNumber,
    int PageSize = PagedRequest.DefaultPageSize) : PagedRequest, IQuery<PagedResult<MovieDto>>
{
    public new int PageNumber { get; init; } = PageNumber;
    public new int PageSize { get; init; } = PageSize;
}
