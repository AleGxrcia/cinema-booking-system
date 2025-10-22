using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieDeactivatedEventHandler : INotificationHandler<MovieDeactivatedEvent>
{
    private readonly ILogger<MovieDeactivatedEventHandler> _logger;

    public MovieDeactivatedEventHandler(ILogger<MovieDeactivatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieDeactivatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Movie deactivated: {MovieId}. Deactivated at: {DeactivatedAt}",
            notification.MovieId,
            notification.DeactivatedAt);

        await Task.CompletedTask;
    }
}