using CinemaBookingSystem.Shared.Application.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : Result
{
}
