using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.DeleteMovie;

public sealed record DeleteMovieCommand(Guid MovieId) : ICommand;
