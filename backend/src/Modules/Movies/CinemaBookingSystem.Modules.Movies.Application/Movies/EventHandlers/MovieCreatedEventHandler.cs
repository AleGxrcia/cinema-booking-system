using CinemaBookingSystem.Modules.Movies.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CinemaBookingSystem.Modules.Movies.Application.Movies.EventHandlers;

public sealed class MovieCreatedEventHandler : INotificationHandler<MovieCreatedEvent>
{
    private readonly ILogger<MovieCreatedEventHandler> _logger;

    public MovieCreatedEventHandler(ILogger<MovieCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(MovieCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Movie created: {MovieId} - {Title} ({Year}), Language: {Language}, Country: {Country}, Rating: {AgeRating}, Status: {Status} at {CreatedAt}",
            notification.MovieId,
            notification.Title.Value,
            notification.ReleaseYear,
            notification.Language.Name,
            notification.Country,
            notification.AgeRating,
            notification.Status,
            notification.CreatedAt);

        await Task.CompletedTask;
    }
}
