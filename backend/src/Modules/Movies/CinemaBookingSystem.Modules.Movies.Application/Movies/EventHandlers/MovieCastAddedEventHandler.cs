using System;
using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieCastAddedEventHandler : INotificationHandler<MovieCastAddedEvent>
{
    private readonly ILogger<MovieCastAddedEventHandler> _logger;

    public MovieCastAddedEventHandler(ILogger<MovieCastAddedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieCastAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
        "Cast member added to movie: MovieId={MovieId}, Person={PersonName}, Role={Role}, Order={Order} at {AddedAt}",
            notification.MovieId,
            notification.PersonName,
            notification.Role,
            notification.Order,
            notification.AddedAt);

        await Task.CompletedTask;
    }
}