using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieCastRemovedEvent(Guid movieId, Guid castId, string personName, DateTime removedAt) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public Guid CastId { get; init; } = castId;
    public string PersonName { get; init; } = personName;
    public DateTime RemovedAt { get; init; } = removedAt;
}
