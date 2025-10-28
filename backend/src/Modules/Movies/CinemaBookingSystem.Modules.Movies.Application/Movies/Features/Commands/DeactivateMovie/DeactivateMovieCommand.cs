using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.DeactivateMovie;

public sealed record DeactivateMovieCommand(Guid MovieId) : ICommand;
