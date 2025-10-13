using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.DeleteGenre;

public sealed record DeleteGenreCommand(Guid GenreId) : ICommand;
