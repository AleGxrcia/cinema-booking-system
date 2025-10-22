using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.EventHandlers;

public sealed class GenreUpdatedEventHandler : INotificationHandler<GenreUpdatedEvent>
{
    private readonly ILogger<GenreUpdatedEventHandler> _logger;

    public GenreUpdatedEventHandler(ILogger<GenreUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(GenreUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre updated: {GenreId} - Changed from '{PreviousName}' to '{NewName}' at {UpdatedAt}",
            notification.GenreId,
            notification.PreviousName,
            notification.NewName,
            notification.UpdatedAt);

        await Task.CompletedTask;
    }
}