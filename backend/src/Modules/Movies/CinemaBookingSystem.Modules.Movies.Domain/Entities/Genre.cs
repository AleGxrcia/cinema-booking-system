using CinemaBookingSystem.Modules.Movies.Domain.Events;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class Genre : AggregateRoot<Guid>
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private Genre()
    {
    }

    public Genre(Guid id, string name) : base(id)
    {
        Name = ValidateName(name);
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new GenreCreatedEvent(Id, Name));
    }

    public void UpdateInformation(string name)
    {
        if (!IsActive)
        {
            throw new DomainException("No se puede actualizar un género inactivo");
        }

        Name = ValidateName(name);
        MarkAsUpdated();

        AddDomainEvent(new GenreUpdatedEvent(Id, Name));
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            throw new DomainException("El género ya está inactivo");
        }

        IsActive = false;
        MarkAsUpdated();

        AddDomainEvent(new GenreDeactivatedEvent(Id));
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new DomainException("El género ya está activo");
        }

        IsActive = true;
        MarkAsUpdated();

        AddDomainEvent(new GenreActivatedEvent(Id));
    }

    private static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("El nombre del género no puede estar vacío");
        }

        if (name.Length < 2)
        {
            throw new DomainException("El nombre del género debe tener al menos 2 caracteres");
        }

        if (name.Length > 50)
        {
            throw new DomainException("El nombre del género no puede tener más de 50 caracteres");
        }

        return name.Trim();
    }

}
