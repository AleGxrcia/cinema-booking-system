using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieActivatedEventHandler : INotificationHandler<MovieActivatedEvent>
{
    private readonly ILogger<MovieActivatedEventHandler> _logger;

    public MovieActivatedEventHandler(ILogger<MovieActivatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieActivatedEvent notification, CancellationToken cancellationEvent)
    {
        _logger.LogInformation(
            "Movie activated: {MovieId}. Activated at: {ActivatedAt}",
            notification.MovieId,
            notification.ActivatedAt);

        await Task.CompletedTask;
    }
}