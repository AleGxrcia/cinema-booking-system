using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.ActivateMovie;

public sealed record ActivateMovieCommand(Guid MovieId) : ICommand;
