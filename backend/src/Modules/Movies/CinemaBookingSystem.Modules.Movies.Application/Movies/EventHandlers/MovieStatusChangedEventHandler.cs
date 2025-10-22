using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieStatusChangedEventHandler : INotificationHandler<MovieStatusChangedEvent>
{
    private readonly ILogger<MovieStatusChangedEventHandler> _logger;

    public MovieStatusChangedEventHandler(ILogger<MovieStatusChangedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Movie status changed: {MovieId}. From {PreviousStatus} to {NewStatus} at {ChangedAt}",
            notification.MovieId,
            notification.PreviousStatus,
            notification.NewStatus,
            notification.ChangedAt);

        await Task.CompletedTask;
    }
}