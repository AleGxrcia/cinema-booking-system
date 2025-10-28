using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.DeactivateGenre;

public sealed record DeactivateGenreCommand(Guid GenreId) : ICommand;
