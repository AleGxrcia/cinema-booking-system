using CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddMoviesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MovieDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
