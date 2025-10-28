using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageGenres;

public sealed record RemoveMovieGenreCommand(
    Guid MovieId,
    Guid GenreId) : ICommand;
