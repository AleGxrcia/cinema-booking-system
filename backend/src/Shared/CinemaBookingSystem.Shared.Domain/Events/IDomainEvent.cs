using MediatR;

namespace CinemaBookingSystem.Shared.Domain.Events;

public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OcurredOn { get; }
    string EventType { get; }
}
