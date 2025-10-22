using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Genres.EventHandlers;

public sealed class GenreCreatedEventHandler : INotificationHandler<GenreCreatedEvent>
{
    private readonly ILogger<GenreCreatedEventHandler> _logger;

    public GenreCreatedEventHandler(ILogger<GenreCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(GenreCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Genre created: {GenreId} - {GenreName} at {CreatedAt}",
            notification.GenreId,
            notification.GenreName,
            notification.CreatedAt);

        await Task.CompletedTask;
    }
}