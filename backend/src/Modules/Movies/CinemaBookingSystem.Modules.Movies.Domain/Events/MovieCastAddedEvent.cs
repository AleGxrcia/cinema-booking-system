using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieCastAddedEvent(Guid movieId, Guid castId, string personName, CastRole role) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid CastId { get; init; } = castId;
    public string PersonName { get; init; } = personName;
    public CastRole Role { get; init; } = role;
}
