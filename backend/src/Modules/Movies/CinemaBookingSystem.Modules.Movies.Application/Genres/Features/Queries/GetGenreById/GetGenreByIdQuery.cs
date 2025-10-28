using CinemaBookingSystem.Modules.Movies.Application.Genres.Dtos;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Queries.GetGenreById;

public sealed record GetGenreByIdQuery(Guid GenreId) : IQuery<GenreDto>;
