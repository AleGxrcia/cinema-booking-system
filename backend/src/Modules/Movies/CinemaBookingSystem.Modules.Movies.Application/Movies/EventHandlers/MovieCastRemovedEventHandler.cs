using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieCastRemovedEventHandler : INotificationHandler<MovieCastRemovedEvent>
{
    private readonly ILogger<MovieCastRemovedEventHandler> _logger;

    public MovieCastRemovedEventHandler(ILogger<MovieCastRemovedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieCastRemovedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Cast member removed from movie: MovieId={MovieId}, CastId={CastId}, PersonName={PersonName} at {RemovedAt}",
            notification.MovieId,
            notification.CastId,
            notification.PersonName,
            notification.RemovedAt);
    
        await Task.CompletedTask;
    }
}