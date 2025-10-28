using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Modules.Movies.Domain.Errors;
using CinemaBookingSystem.Modules.Movies.Domain.Repositories;
using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.CreateGenre;

public sealed class CreateGenreCommandHandler : ICommandHandler<CreateGenreCommand, Result<Guid>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var nameExists = await _genreRepository.ExistsByNameAsync(request.Name, cancellationToken);
        if (nameExists)
            return GenreErrors.NameAlreadyExists(request.Name);

        var genreResult = Genre.Create(Guid.NewGuid(), request.Name);
        if (genreResult.IsFailure)
            return genreResult.Error;

        _genreRepository.Add(genreResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return genreResult.Value.Id;
    }
}
