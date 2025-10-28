using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageCast;

public sealed class AddMovieCastCommandHandler : ICommandHandler<AddMovieCastCommand>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMovieCastCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddMovieCastCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdWithDetailsAsync(
            request.MovieId, 
            cancellationToken);

        if (movie is null)
            return MovieErrors.NotFound(request.MovieId);

        var result = movie.AddCastMember(request.PersonName, request.Role, request.Order);
        if (result.IsFailure)
            return result.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
