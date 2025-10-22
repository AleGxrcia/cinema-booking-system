using CinemaBookingSystem.Modules.Movies.Application.Movies.Dtos;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMovieById;

public sealed record GetMovieByIdQuery(Guid MovieId) : IQuery<MovieDto>;
