using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieGenreRemovedEventHandler : INotificationHandler<MovieGenreRemovedEvent>
{
    private readonly ILogger<MovieGenreRemovedEventHandler> _logger;

    public MovieGenreRemovedEventHandler(ILogger<MovieGenreRemovedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieGenreRemovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre '{GenreName}' removed from movie: MovieId={MovieId}, GenreId={GenreId} at {RemovedAt}",
            notification.GenreName,
            notification.MovieId,
            notification.GenreId,
            notification.RemovedAt);

        await Task.CompletedTask;
    }
}