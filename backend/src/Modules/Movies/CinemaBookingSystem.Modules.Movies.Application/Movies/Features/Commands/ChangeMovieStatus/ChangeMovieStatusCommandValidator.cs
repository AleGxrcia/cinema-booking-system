using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ChangeMovieStatus;

public sealed class ChangeMovieStatusCommandValidator : AbstractValidator<ChangeMovieStatusCommand>
{
    public ChangeMovieStatusCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .WithMessage("Movie ID is required");

        RuleFor(x => x.NewStatus)
            .IsInEnum()
            .WithMessage("Invalid movie status");
    }
}
