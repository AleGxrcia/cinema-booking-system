using CinemaBookingSystem.Shared.Domain.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Result>
    where TCommand : ICommand<Result>
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
}
