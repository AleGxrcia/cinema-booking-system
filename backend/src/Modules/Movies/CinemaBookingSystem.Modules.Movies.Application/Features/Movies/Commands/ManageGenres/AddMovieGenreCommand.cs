using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Movies.Commands.ManageGenres;

public sealed record AddMovieGenreCommand(
    Guid MovieId,
    Guid GenreId) : ICommand;
