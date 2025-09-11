using System;
using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreActivatedEvent(Guid genreId) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
}
