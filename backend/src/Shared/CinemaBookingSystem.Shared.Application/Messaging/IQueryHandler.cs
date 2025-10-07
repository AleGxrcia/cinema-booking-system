using CinemaBookingSystem.Shared.Domain.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : Result
{
}
