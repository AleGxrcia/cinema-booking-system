using MediatR;

namespace CinemaBookingSystem.Modules.Showtimes.Application.Commands.CreatePricingPolicy;

using CinemaBookingSystem.Shared.Domain;

public sealed record CreatePricingPolicyCommand(
    string Name,
    IEnumerable<PricingRuleDto> Rules) : IRequest<Result>;

public sealed record PricingRuleDto(
    int SeatTypeId,
    int HallFormatTypeId,
    decimal Price);
