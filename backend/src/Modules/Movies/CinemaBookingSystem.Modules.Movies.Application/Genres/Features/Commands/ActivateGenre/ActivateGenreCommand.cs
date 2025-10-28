using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.Features.Commands.ActivateGenre;

public sealed record ActivateGenreCommand(Guid GenreId) : ICommand;
