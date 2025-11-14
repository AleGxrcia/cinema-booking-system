using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Shared.Domain;
using MediatR;

namespace CinemaBookingSystem.Modules.Showtimes.Application.Commands.CreatePricingPolicy;

internal sealed class CreatePricingPolicyCommandHandler : IRequestHandler<CreatePricingPolicyCommand, Result>
{
    private readonly IPricingPolicyRepository _pricingPolicyRepository;

    public CreatePricingPolicyCommandHandler(IPricingPolicyRepository pricingPolicyRepository)
    {
        _pricingPolicyRepository = pricingPolicyRepository;
    }

    public async Task<Result> Handle(CreatePricingPolicyCommand request, CancellationToken cancellationToken)
    {
        var policy = PricingPolicy.Create(request.Name);

        foreach (var ruleDto in request.Rules)
        {
            if (!Enum.IsDefined(typeof(SeatType), ruleDto.SeatTypeId))
            {
                return Result.Failure(new Error("CreatePricingPolicy.InvalidSeatType", $"Invalid SeatTypeId: {ruleDto.SeatTypeId}"));
            }

            if (!Enum.IsDefined(typeof(HallFormatType), ruleDto.HallFormatTypeId))
            {
                return Result.Failure(new Error("CreatePricingPolicy.InvalidHallFormatType", $"Invalid HallFormatTypeId: {ruleDto.HallFormatTypeId}"));
            }

            var rule = PricingRule.Create(
                (SeatType)ruleDto.SeatTypeId,
                (HallFormatType)ruleDto.HallFormatTypeId,
                ruleDto.Price);

            policy.AddRule(rule);
        }

        await _pricingPolicyRepository.AddAsync(policy);

        return Result.Success();
    }
}
