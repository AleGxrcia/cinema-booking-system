using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.UpdateGenre;

public sealed class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.GenreId)
            .NotEmpty()
            .WithMessage("Genre ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Genre name is required")
            .MinimumLength(2)
            .WithMessage("Genre name must have at least 2 characters")
            .MaximumLength(50)
            .WithMessage("Genre name cannot exceed 50 characters");
    }
}
