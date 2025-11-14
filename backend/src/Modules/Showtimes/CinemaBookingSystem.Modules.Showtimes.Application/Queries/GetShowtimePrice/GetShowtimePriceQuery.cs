using CinemaBookingSystem.Shared.Domain;
using MediatR;

namespace CinemaBookingSystem.Modules.Showtimes.Application.Queries.GetShowtimePrice;

public sealed record GetShowtimePriceQuery(
    Guid ShowtimeId,
    int SeatTypeId) : IRequest<Result<ShowtimePriceDto>>;

public sealed record ShowtimePriceDto(
    decimal Price);
