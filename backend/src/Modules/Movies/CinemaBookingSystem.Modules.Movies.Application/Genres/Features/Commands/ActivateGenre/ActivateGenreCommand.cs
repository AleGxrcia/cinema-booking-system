using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.ActivateGenre;

public sealed record ActivateGenreCommand(Guid GenreId) : ICommand;
