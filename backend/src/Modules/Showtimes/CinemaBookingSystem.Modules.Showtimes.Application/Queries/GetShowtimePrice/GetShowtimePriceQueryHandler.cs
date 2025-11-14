using CinemaBookingSystem.Modules.Showtimes.Domain.Enums;
using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Domain.Services;
using CinemaBookingSystem.Shared.Domain;
using MediatR;

namespace CinemaBookingSystem.Modules.Showtimes.Application.Queries.GetShowtimePrice;

internal sealed class GetShowtimePriceQueryHandler : IRequestHandler<GetShowtimePriceQuery, Result<ShowtimePriceDto>>
{
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly IPricingService _pricingService;

    public GetShowtimePriceQueryHandler(IShowtimeRepository showtimeRepository, IPricingService pricingService)
    {
        _showtimeRepository = showtimeRepository;
        _pricingService = pricingService;
    }

    public async Task<Result<ShowtimePriceDto>> Handle(GetShowtimePriceQuery request, CancellationToken cancellationToken)
    {
        var showtime = await _showtimeRepository.GetByIdAsync(request.ShowtimeId);

        if (showtime is null)
        {
            return Result.Failure<ShowtimePriceDto>(new Error("GetShowtimePrice.ShowtimeNotFound", "Showtime not found."));
        }

        var priceResult = await _pricingService.CalculatePrice(showtime, (SeatType)request.SeatTypeId);

        if (priceResult.IsFailure)
        {
            return Result.Failure<ShowtimePriceDto>(priceResult.Error);
        }

        return new ShowtimePriceDto(priceResult.Value);
    }
}
