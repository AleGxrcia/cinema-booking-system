using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class MovieCast : Entity<Guid>
{
    public Guid MovieId { get; private set; }
    public string PersonName { get; private set; } = default!;
    public CastRole Role { get; private set; }
    public int Order { get; private set; }
    public Movie Movie { get; private set; } = default!;

    private MovieCast()
    {
    }

    public MovieCast(Guid id, Guid movieId, string personName, CastRole role, int order = 0) : base(id)
    {
        if (movieId == Guid.Empty)
        {
            throw new DomainException("El ID de la película es obligatorio");
        }

        MovieId = movieId;
        PersonName = ValidatePersonName(personName);
        Role = role;
        Order = ValidateOrder(order);
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateInformation(string personName, CastRole role, int order)
    {
        PersonName = ValidatePersonName(personName);
        Role = role;
        Order = ValidateOrder(order);
        MarkAsUpdated();
    }

    private static string ValidatePersonName(string actorName)
    {
        if (string.IsNullOrWhiteSpace(actorName))
        {
            throw new DomainException("El nombre del actor no puede estar vacío");
        }

        if (actorName.Length < 2)
        {
            throw new DomainException("El nombre del actor debe tener al menos 2 caracteres");
        }

        if (actorName.Length > 100)
        {
            throw new DomainException("El nombre del actor no puede tener más de 100 caracteres");
        }

        return actorName.Trim();
    }

    private static int ValidateOrder(int order)
    {
        if (order < 0)
        {
            throw new DomainException("El orden no puede ser negativo");
        }

        return order;
    }
}
