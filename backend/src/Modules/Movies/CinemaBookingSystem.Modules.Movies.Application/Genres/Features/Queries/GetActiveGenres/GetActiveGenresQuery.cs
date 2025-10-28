using CinemaBookingSystem.Modules.Movies.Application.Genres.Dtos;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Queries.GetActiveGenres;

public sealed record GetActiveGenresQuery : IQuery<IReadOnlyList<GenreDto>>;