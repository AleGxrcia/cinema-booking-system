using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieUpdatedEventHandler : INotificationHandler<MovieUpdatedEvent>
{
    private readonly ILogger<MovieUpdatedEventHandler> _logger;

    public MovieUpdatedEventHandler(ILogger<MovieUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Movie updated: {MovieId} - {Title}. Updated at: {UpdatedAt}",
            notification.MovieId,
            notification.Title.Value,
            notification.UpdatedAt);

        await Task.CompletedTask;
    }
}