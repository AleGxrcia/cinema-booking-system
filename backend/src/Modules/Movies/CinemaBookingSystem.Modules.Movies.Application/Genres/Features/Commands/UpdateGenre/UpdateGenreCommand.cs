using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.UpdateGenre;

public sealed record UpdateGenreCommand(Guid GenreId, string Name) : ICommand;
