using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieGenreAddedEventHandler : INotificationHandler<MovieGenreAddedEvent>
{
    private readonly ILogger<MovieGenreAddedEventHandler> _logger;

    public MovieGenreAddedEventHandler(ILogger<MovieGenreAddedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieGenreAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre '{GenreName}' added to movie: MovieId={MovieId}, GenreId={GenreId} at {AddedAt}",
            notification.GenreName,
            notification.MovieId,
            notification.GenreId,
            notification.AddedAt);

        await Task.CompletedTask;
    }
}