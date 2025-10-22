using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Modules.Movies.Domain.ValueObjects;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.UpdateMovie;

public sealed class UpdateMovieCommandHandler : ICommandHandler<UpdateMovieCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.MovieId, cancellationToken);
        if (movie is null)
            return MovieErrors.NotFound(request.MovieId);

        var titleResult = MovieTitle.Create(request.Title);
        if (titleResult.IsFailure)
            return titleResult.Error;

        var durationResult = Duration.FromMinutes(request.DurationInMinutes);
        if (durationResult.IsFailure)
            return durationResult.Error;

        var exists = await _movieRepository.ExistsByTitleAndYearAsync(
            titleResult.Value.NormalizedValue,
            movie.ReleaseYear,
            movie.Id,
            cancellationToken
        );

        var updateResult = movie.UpdateInformation(
            titleResult.Value,
            request.Description,
            durationResult.Value,
            request.PosterUrl,
            request.TrailerUrl);

        if (updateResult.IsFailure)
            return updateResult.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
