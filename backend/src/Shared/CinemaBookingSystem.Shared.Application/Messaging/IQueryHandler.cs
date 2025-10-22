using CinemaBookingSystem.Shared.Domain.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;