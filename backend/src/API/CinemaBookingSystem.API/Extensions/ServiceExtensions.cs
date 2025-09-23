
using System.Reflection;
using CinemaBookingSystem.Modules.Movies.Extensions;
using MediatR;

namespace CinemaBookingSystem.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddModules(
        this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        services.AddMediatRWithAssemblies(assemblies);
        services.AddMoviesModule(configuration);

        return services;
    }

    private static void AddMediatRWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
        });
    }
}
