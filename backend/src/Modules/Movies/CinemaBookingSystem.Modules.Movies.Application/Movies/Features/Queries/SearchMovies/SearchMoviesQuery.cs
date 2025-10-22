using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Common;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.SearchMovies;

public sealed record SearchMoviesQuery(
    string? SearchTerm = null,
    int? ReleaseYear = null,
    MovieStatus? Status = null,
    AgeRating? AgeRating = null,
    Guid? GenreId = null,
    string? Country = null,
    int PageNumber = PagedRequest.MinPageNumber,
    int PageSize = PagedRequest.DefaultPageSize) : PagedRequest, IQuery<PagedResult<MovieDto>>
{
    public new int PageNumber { get; init; } = PageNumber;
    public new int PageSize { get; init; } = PageSize;
}