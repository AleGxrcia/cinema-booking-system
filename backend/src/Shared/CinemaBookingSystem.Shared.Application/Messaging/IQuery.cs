using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
