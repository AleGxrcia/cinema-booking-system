using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.EventHandlers;

public sealed class GenreDeactivatedEventHandler : INotificationHandler<GenreDeactivatedEvent>
{
    private readonly ILogger<GenreDeactivatedEventHandler> _logger;

    public GenreDeactivatedEventHandler(ILogger<GenreDeactivatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(GenreDeactivatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre deactivated: {GenreId} at {DeactivatedAt}",
            notification.GenreId,
            notification.DeactivatedAt);

        await Task.CompletedTask;
    }
}
