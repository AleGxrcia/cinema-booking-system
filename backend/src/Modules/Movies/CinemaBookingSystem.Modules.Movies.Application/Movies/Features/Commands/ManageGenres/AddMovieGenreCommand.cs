using CinemaBookingSystem.Shared.Application.Messaging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.Features.Commands.ManageGenres;

public sealed record AddMovieGenreCommand(
    Guid MovieId,
    Guid GenreId) : ICommand;
