using CinemaBookingSystem.Modules.Showtimes.Domain.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Domain.Services;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Repositories;
using CinemaBookingSystem.Modules.Showtimes.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddShowtimesInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShowtimesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<IPricingPolicyRepository, PricingPolicyRepository>();
        services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
        services.AddScoped<IPricingService, PricingService>();

        return services;
    }
}
