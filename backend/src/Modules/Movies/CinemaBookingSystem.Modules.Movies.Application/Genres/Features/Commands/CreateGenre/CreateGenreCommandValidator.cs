using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.CreateGenre;

public sealed class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Genre name is required")
            .MinimumLength(2)
            .WithMessage("Genre name must have at least 3 characters")
            .MaximumLength(50)
            .WithMessage("Genre name cannot exceed 50 characters");
    }
}
