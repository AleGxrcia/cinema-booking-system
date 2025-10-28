using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.UpdateGenre;

public sealed class UpdateGenreCommandHandler : ICommandHandler<UpdateGenreCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.GenreId, cancellationToken);
        if (genre == null)
            return GenreErrors.NotFound(request.GenreId);

        var nameExists = await _genreRepository.ExistsByNameAsync(
            request.Name,
            request.GenreId,
            cancellationToken);

        if (nameExists)
            return GenreErrors.NameAlreadyExists(request.Name);

        var updateResult = genre.UpdateInformation(request.Name);
        if (updateResult.IsFailure)
            return updateResult.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
