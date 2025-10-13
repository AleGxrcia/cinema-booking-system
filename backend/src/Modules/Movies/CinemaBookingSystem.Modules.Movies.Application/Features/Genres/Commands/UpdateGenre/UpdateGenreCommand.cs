using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.UpdateGenre;

public sealed record UpdateGenreCommand(Guid GenreId, string Name) : ICommand;
