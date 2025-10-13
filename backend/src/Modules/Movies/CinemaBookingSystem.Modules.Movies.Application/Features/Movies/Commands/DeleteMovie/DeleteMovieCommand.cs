using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.DeleteMovie;

public sealed record DeleteMovieCommand(Guid MovieId) : ICommand;
