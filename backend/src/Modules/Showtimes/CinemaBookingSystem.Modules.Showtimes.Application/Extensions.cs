using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CinemaBookingSystem.Modules.Showtimes.Application;

public static class Extensions
{
    public static IServiceCollection AddShowtimesApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
