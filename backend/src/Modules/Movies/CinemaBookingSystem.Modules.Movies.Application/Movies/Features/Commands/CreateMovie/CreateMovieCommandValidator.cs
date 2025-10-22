using FluentValidation;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.CreateMovie;

public sealed class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
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

        RuleFor(x => x.ReleaseYear)
             .GreaterThanOrEqualTo(1888)
             .WithMessage("Release year cannot be earlier than 1888")
             .LessThanOrEqualTo(DateTime.Now.Year + 5)
             .WithMessage("Release year cannot be more than 5 years in the future");

        RuleFor(x => x.Language)
            .NotEmpty()
            .WithMessage("Language is required")
            .MaximumLength(50)
            .WithMessage("Language cannot exceed 50 characters");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required")
            .MinimumLength(2)
            .WithMessage("Country must have at least 2 characters")
            .MaximumLength(100)
            .WithMessage("Country cannot exceed 100 characters");

        RuleFor(x => x.AgeRating)
            .IsInEnum()
            .WithMessage("Invalid age rating");

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

        When(x => x.GenreIds != null && x.GenreIds.Count > 0, () =>
        {
            RuleFor(x => x.GenreIds)
                .Must(ids => ids!.All(id => id != Guid.Empty))
                .WithMessage("All genre IDs must be valid");
        });
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
