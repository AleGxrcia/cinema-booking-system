using CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;
using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.ManageCast;

public sealed class AddMovieCastCommandValidator : AbstractValidator<AddMovieCastCommand>
{
    public AddMovieCastCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .WithMessage("Movie ID is required");

        RuleFor(x => x.PersonName)
            .NotEmpty()
            .WithMessage("Person name is required")
            .MaximumLength(100)
            .WithMessage("Person name cannot exceed 100 characters");

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage("Invalid cast role");
    }
}
