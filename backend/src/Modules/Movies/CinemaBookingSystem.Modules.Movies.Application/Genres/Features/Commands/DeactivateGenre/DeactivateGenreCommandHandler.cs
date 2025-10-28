using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.DeactivateGenre;

public sealed class DeactivateGenreCommandHandler : ICommandHandler<DeactivateGenreCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeactivateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);
        if (genre is null)
            return GenreErrors.NotFound(request.GenreId);

        var result = genre.Deactivate();
        if (result.IsFailure)
            return result.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
