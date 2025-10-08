using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class MovieCast : Entity<Guid>
{
    private const int MinPersonNameLength = 2;
    private const int MaxPersonNameLength = 100;

    public Guid MovieId { get; private set; }
    public string PersonName { get; private set; } = default!;
    public CastRole Role { get; private set; }
    public int Order { get; private set; }
    public Movie Movie { get; private set; } = default!;

    private MovieCast()
    {
    }

    private MovieCast(Guid id, Guid movieId, string personName, CastRole role, int order = 0) : base(id)
    {
        MovieId = movieId;
        PersonName = personName;
        Role = role;
        Order = order;
        CreatedAt = DateTime.UtcNow;
    }
    
    public static Result<MovieCast> Create(Guid id, Guid movieId, string personName, CastRole role, int order = 0)
    {
        if (movieId == Guid.Empty)
            return MovieCastErrors.MovieIdEmpty();

        var personNameResult = ValidatePersonName(personName);
        if (personNameResult.IsFailure)
            return personNameResult.Error;

        var orderResult = ValidateOrder(order);
        if (orderResult.IsFailure)
            return orderResult.Error;

        return new MovieCast(id, movieId, personNameResult.Value, role, orderResult.Value);
    }

    public Result UpdateInformation(string personName, CastRole role, int order)
    {
        var personNameResult = ValidatePersonName(personName);
        if (personNameResult.IsFailure)
            return personNameResult.Error;

        var orderResult = ValidateOrder(order);
        if (orderResult.IsFailure)
            return orderResult.Error;

        PersonName = personNameResult.Value;
        Role = role;
        Order = orderResult.Value;
        MarkAsUpdated();

        return Result.Success();
    }

    private static Result<string> ValidatePersonName(string personName)
    {
        if (string.IsNullOrWhiteSpace(personName))
            return MovieCastErrors.PersonNameEmpty();

        if (personName.Length < MinPersonNameLength)
            return MovieCastErrors.PersonNameTooShort(MinPersonNameLength);

        if (personName.Length > MaxPersonNameLength)
            return MovieCastErrors.PersonNameTooLong(MaxPersonNameLength);

        return personName.Trim();
    }

    private static Result<int> ValidateOrder(int order)
    {
        if (order < 0)
            return MovieCastErrors.OrderNegative();

        return order;
    }
}
