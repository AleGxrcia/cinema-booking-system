using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class MovieStatusChangedEvent(Guid movieId, MovieStatus previousStatus, MovieStatus newStatus) : DomainEventBase
{
    public Guid MovieId { get; init; } = movieId;
    public MovieStatus PreviousStatus { get; init; } = previousStatus;
    public MovieStatus NewStatus { get; init; } = newStatus;
}
