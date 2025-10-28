using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;

public sealed record RemoveMovieCastCommand(
    Guid MovieId,
    Guid CastId) : ICommand;
