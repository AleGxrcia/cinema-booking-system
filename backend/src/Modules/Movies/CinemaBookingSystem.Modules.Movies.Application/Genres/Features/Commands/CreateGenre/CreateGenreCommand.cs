using CinemaBookingSystem.Shared.Application.Messaging;
using CinemaBookingSystem.Shared.Domain.Common;

namespace CinemaBookingSystem.Modules.Movies.Application.Features.Genres.Commands.CreateGenre;

public sealed record CreateGenreCommand(string Name) : ICommand<Result<Guid>>;