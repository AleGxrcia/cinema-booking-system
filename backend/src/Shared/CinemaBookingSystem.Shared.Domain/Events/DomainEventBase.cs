namespace CinemaBookingSystem.Shared.Domain.Events;

public abstract class DomainEventBase : IDomainEvent
{
    public Guid EventId { get; private set; }
    public DateTime OcurredOn { get; private set; }
    public string EventType => GetType().Name;

    protected DomainEventBase()
    {
        EventId = Guid.NewGuid();
        OcurredOn = DateTime.UtcNow;
    }
}
