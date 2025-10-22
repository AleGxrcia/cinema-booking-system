using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Queries.GetMovieDetails;

public sealed record GetMovieDetailsQuery(Guid MovieId) : IQuery<MovieDetailsDto>;
