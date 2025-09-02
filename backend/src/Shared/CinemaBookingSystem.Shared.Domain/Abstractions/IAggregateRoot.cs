using CinemaBookingSystem.Shared.Domain.Events;

namespace CinemaBookingSystem.Shared.Domain.Abstractions;

public interface IAggregateRoot<TId> : IEntity<TId>
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
