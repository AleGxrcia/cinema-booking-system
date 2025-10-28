using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ActivateMovie;

public sealed record ActivateMovieCommand(Guid MovieId) : ICommand;
