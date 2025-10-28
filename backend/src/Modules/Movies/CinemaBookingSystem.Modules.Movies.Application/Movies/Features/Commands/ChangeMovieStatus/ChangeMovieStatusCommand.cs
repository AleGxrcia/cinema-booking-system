using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ChangeMovieStatus;

public sealed record ChangeMovieStatusCommand(
    Guid MovieId,
    MovieStatus NewStatus) : ICommand;