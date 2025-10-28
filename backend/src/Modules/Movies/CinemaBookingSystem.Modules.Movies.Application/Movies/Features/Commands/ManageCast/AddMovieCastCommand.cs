using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;

public sealed record AddMovieCastCommand(
    Guid MovieId,
    string PersonName,
    CastRole Role,
    int Order) : ICommand;
