using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.DeactivateMovie;

public sealed record DeactivateMovieCommand(Guid MovieId) : ICommand;
