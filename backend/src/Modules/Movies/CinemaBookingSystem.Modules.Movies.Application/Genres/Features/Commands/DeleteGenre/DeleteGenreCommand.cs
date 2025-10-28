using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.DeleteGenre;

public sealed record DeleteGenreCommand(Guid GenreId) : ICommand;
