
namespace CinemaBookingSystem.Shared.Domain.Abstractions;

public abstract class Entity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; }
    public DateTime? CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    protected Entity()
    {
    }

    public virtual void MarkAsUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
