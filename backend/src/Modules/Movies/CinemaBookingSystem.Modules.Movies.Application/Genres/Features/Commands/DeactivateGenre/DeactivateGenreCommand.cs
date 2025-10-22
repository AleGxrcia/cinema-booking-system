using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.DeactivateGenre;

public sealed record DeactivateGenreCommand(Guid GenreId) : ICommand;
