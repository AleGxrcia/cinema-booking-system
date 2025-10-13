using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.ManageCast;

public sealed record AddMovieCastCommand(
    Guid MovieId,
    string PersonName,
    CastRole Role) : ICommand;