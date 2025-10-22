using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Events;
using CinemaBookingSystem.Shared.Domain.Abstractions;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Domain.Entities;

public class Genre : AggregateRoot<Guid>
{
    private const int MinNameLength = 2;
    private const int MaxNameLength = 50;

    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private Genre()
    {
    }

    private Genre(Guid id, string name) : base(id)
    {
        Name = name;
        IsActive = true;
    }

    public static Result<Genre> Create(Guid id, string name)
    {
        var nameResult = ValidateName(name);
        if (nameResult.IsFailure)
            return nameResult.Error;

        var genre = new Genre(id, nameResult.Value);
        genre.AddDomainEvent(new GenreCreatedEvent(id, nameResult.Value, genre.CreatedAt));

        return genre;
    }

    public Result UpdateInformation(string name)
    {
        if (!IsActive)
            return GenreErrors.CannotUpdateInactive();

        var nameResult = ValidateName(name);
        if (nameResult.IsFailure)
            return nameResult.Error;

        var previousName = Name;
        Name = nameResult.Value;
        MarkAsUpdated();
        AddDomainEvent(new GenreUpdatedEvent(Id, previousName, Name, UpdatedAt!.Value));

        return Result.Success();
    }


    public Result Activate()
    {
        if (IsActive)
            return GenreErrors.AlreadyActive();

        IsActive = true;
        MarkAsUpdated();
        AddDomainEvent(new GenreActivatedEvent(Id, UpdatedAt!.Value));

        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return GenreErrors.AlreadyInactive();

        IsActive = false;
        MarkAsUpdated();
        AddDomainEvent(new GenreDeactivatedEvent(Id, UpdatedAt!.Value));

        return Result.Success();
    }

    private static Result<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GenreErrors.NameEmpty();

        if (name.Length < MinNameLength)
            return GenreErrors.NameTooShort(MinNameLength);

        if (name.Length > MaxNameLength)
            return GenreErrors.NameTooLong(MaxNameLength);

        return name.Trim();
    }
}
