using CinemaBookingSystem.Modules.Movies.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBookingSystem.Modules.Movies.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddMoviesModule(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMoviesInfrastructure(configuration);
        return services;
    }

    public static IApplicationBuilder UseMoviesModule(this IApplicationBuilder app)
    {
        return app;
    }
}
