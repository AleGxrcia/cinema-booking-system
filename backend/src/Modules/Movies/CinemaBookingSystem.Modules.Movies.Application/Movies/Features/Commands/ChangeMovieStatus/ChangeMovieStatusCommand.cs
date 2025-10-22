using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.ChangeMovieStatus;

public sealed record ChangeMovieStatusCommand(
    Guid MovieId,
    MovieStatus NewStatus) : ICommand;