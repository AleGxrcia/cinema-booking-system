using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Domain.Services;
using CinemaBookingSystem.Shared.Domain;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Services;

public class PricingService : IPricingService
{
    private readonly IPricingPolicyRepository _pricingPolicyRepository;
    private readonly IHallRepository _hallRepository;

    public PricingService(
        IPricingPolicyRepository pricingPolicyRepository,
        IHallRepository hallRepository)
    {
        _pricingPolicyRepository = pricingPolicyRepository;
        _hallRepository = hallRepository;
    }

    public async Task<Result<decimal>> CalculatePrice(Showtime showtime, SeatType seatType)
    {
        var policy = await _pricingPolicyRepository.GetByIdAsync(showtime.PricingPolicyId);

        if (policy is null)
        {
            return Result.Failure<decimal>(new Error("PricingService.PolicyNotFound", "Pricing policy not found."));
        }

        var hall = await _hallRepository.GetByIdAsync(showtime.HallId);

        if (hall is null)
        {
            return Result.Failure<decimal>(new Error("PricingService.HallNotFound", "Hall not found."));
        }

        var rule = policy.GetRule(seatType, hall.Format);

        if (rule is null)
        {
            return Result.Failure<decimal>(new Error("PricingService.RuleNotFound", "Pricing rule not found for the given criteria."));
        }

        return rule.Price;
    }
}
