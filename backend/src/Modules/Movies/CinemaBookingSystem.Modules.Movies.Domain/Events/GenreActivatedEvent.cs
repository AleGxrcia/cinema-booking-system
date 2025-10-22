using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreActivatedEvent : DomainEventBase
{
    public Guid GenreId { get; init; }
    public DateTime ActivatedAt { get; init; }

    public GenreActivatedEvent(Guid genreId, DateTime activatedAt)
    {
        GenreId = genreId;
        ActivatedAt = activatedAt;
    }
}
