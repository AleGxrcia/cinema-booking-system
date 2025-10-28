using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;

public sealed class RemoveMovieCastCommandValidator : AbstractValidator<RemoveMovieCastCommand>
{
    public RemoveMovieCastCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .WithMessage("Movie ID is required");

        RuleFor(x => x.CastId)
            .NotEmpty()
            .WithMessage("Cast ID is required");
    }
}
