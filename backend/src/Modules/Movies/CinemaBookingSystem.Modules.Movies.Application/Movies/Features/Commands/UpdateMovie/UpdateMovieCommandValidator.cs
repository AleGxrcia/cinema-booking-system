using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.UpdateMovie;

public sealed class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.MovieId)
            .NotEmpty()
            .WithMessage("Movie ID is required");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters");

        RuleFor(x => x.DurationInMinutes)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than 0")
            .LessThanOrEqualTo(600)
            .WithMessage("Duration cannot exceed 600 minutes");

        When(x => !string.IsNullOrEmpty(x.PosterUrl), () =>
        {
            RuleFor(x => x.PosterUrl)
                .Must(BeAValidUrl!)
                .WithMessage("Invalid poster URL format");
        });

        When(x => !string.IsNullOrEmpty(x.TrailerUrl), () =>
        {
            RuleFor(x => x.TrailerUrl)
                .Must(BeAValidUrl!)
                .WithMessage("Invalid trailer URL format");
        });
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
