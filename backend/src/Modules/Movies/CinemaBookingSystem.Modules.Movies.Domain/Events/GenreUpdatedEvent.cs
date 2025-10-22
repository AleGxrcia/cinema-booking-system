using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Modules.Movies.Domain.Events;

public sealed class GenreUpdatedEvent(Guid genreId, string previousName, string newName, DateTime updatedAt) : DomainEventBase
{
    public Guid GenreId { get; init; } = genreId;
    public string PreviousName { get; init; } = previousName;
    public string NewName { get; init; } = newName;
    public DateTime UpdatedAt { get; init; } = updatedAt;
}