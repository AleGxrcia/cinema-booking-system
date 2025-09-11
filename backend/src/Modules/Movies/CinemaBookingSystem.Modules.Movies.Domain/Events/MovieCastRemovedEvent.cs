using System;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public class MovieCastRemovedEvent(Guid movieId, Guid castId, string personName) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid CastId { get; init; } = castId;
    public string PersonName { get; init; } = personName;
}
