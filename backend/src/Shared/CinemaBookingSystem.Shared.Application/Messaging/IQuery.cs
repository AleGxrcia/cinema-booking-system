using CinemaBookingSystem.Shared.Domain.Common;
using MediatR;

namespace CinemaBookingSystem.Shared.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
