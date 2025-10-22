using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.EventHandlers;

public sealed class GenreActivatedEventHandler : INotificationHandler<GenreActivatedEvent>
{
    private readonly ILogger<GenreActivatedEventHandler> _logger;

    public GenreActivatedEventHandler(ILogger<GenreActivatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(GenreActivatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre activated: {GenreId} at {ActivatedAt}",
            notification.GenreId,
            notification.ActivatedAt);

        await Task.CompletedTask;
    }
}
