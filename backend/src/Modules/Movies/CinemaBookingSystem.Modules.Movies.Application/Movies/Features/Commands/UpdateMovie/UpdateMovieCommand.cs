using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.UpdateMovie;

public sealed record UpdateMovieCommand(
    Guid MovieId,
    string Title,
    string Description,
    int DurationInMinutes,
    string? PosterUrl = null,
    string? TrailerUrl = null) : ICommand;
