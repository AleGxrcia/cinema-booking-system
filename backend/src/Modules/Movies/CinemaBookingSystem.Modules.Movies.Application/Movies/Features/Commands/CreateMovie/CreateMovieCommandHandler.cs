using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.CreateMovie;

public sealed class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand, Result<Guid>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieCommandHandler(IMovieRepository movieRepository, IGenreRepository genreRepository,
        IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var titleResult = MovieTitle.Create(request.Title);
        if (titleResult.IsFailure)
            return titleResult.Error;

        var durationResult = Duration.FromMinutes(request.DurationInMinutes);
        if (durationResult.IsFailure)
            return durationResult.Error;

        var languageResult = MovieLanguage.Create(request.CodeLanguage, request.Language);
        if (languageResult.IsFailure)
            return languageResult.Error;

        var exists = await _movieRepository.ExistsByTitleAndYearAsync(
            titleResult.Value.NormalizedValue,
            request.ReleaseYear,
            cancellationToken
        );

        if (exists)
            return MovieErrors.DuplicateMovieExists(request.Title, request.ReleaseYear);

        var movieResult = Movie.Create(
            Guid.NewGuid(),
            titleResult.Value,
            request.Description,
            durationResult.Value,
            request.ReleaseYear,
            languageResult.Value,
            request.Country,
            request.AgeRating
        );

        if (movieResult.IsFailure)
            return movieResult.Error;

        var movie = movieResult.Value;

        if (!string.IsNullOrEmpty(request.PosterUrl) || !string.IsNullOrEmpty(request.TrailerUrl))
        {
            var updateResult = movie.UpdateInformation(
                titleResult.Value,
                request.Description,
                durationResult.Value,
                request.PosterUrl,
                request.TrailerUrl);

            if (updateResult.IsFailure)
                return updateResult.Error;
        }

        if (request.GenreIds != null && request.GenreIds.Count > 0)
        {
            var genres = await _genreRepository.GetByIdsAsync(request.GenreIds, cancellationToken);

            if (genres.Count != request.GenreIds.Count)
                return Error.NotFound("Genre.NotFound", "One or more genres were not found");

            foreach (var genre in genres)
            {
                var addGenreResult = movie.AddGenre(genre);
                if (addGenreResult.IsFailure)
                    return addGenreResult.Error;
            }
        }

        _movieRepository.Add(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return movie.Id;
    }
}
