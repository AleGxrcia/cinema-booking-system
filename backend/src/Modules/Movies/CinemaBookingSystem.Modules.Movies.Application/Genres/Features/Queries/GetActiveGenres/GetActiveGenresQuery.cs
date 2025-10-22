using CinemaBookingSystem.Modules.Movies.Application.Genres.Common;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Queries.GetActiveGenres;

public sealed record GetActiveGenresQuery : IQuery<IReadOnlyList<GenreDto>>;