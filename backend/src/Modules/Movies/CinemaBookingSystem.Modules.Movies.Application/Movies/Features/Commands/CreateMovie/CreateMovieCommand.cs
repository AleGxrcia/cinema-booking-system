using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.CreateMovie;

public sealed record CreateMovieCommand(
    string Title,
    string Description,
    int DurationInMinutes,
    int ReleaseYear,
    string CodeLanguage,
    string Language,
    string Country,
    AgeRating AgeRating,
    string? PosterUrl = null,
    string? TrailerUrl = null,
    List<Guid>? GenreIds = null) : ICommand<Result<Guid>>;
