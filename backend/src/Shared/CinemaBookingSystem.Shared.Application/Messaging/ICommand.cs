using CinemaBookingSystem.Shared.Application.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface ICommand : ICommand<Result>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}
