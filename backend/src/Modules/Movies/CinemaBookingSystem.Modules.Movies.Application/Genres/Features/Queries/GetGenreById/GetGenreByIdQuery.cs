using CinemaBookingSystem.Modules.Movies.Application.Genres.Common;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Queries.GetGenreById;

public sealed record GetGenreByIdQuery(Guid GenreId) : IQuery<GenreDto>;
