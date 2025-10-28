using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;

public sealed class RemoveMovieCastCommandHandler : ICommandHandler<RemoveMovieCastCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMovieCastCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemoveMovieCastCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdWithDetailsAsync(request.MovieId, cancellationToken);
        if (movie is null)
            return MovieErrors.NotFound(request.MovieId);

        var result = movie.RemoveCastMember(request.CastId);
        if (result.IsFailure)
            return result.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
