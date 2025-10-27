using CinemaBookingSystem.Shared.Presentation.Contracts.Abstractions;
using CinemaBookingSystem.Shared.Presentation.Http.Factories;
using CinemaBookingSystem.Shared.Presentation.Http.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBookingSystem.Shared.Presentation.Http;

public static class ServiceRegistration
{
    public static IServiceCollection AddSharedPresentationHttp(this IServiceCollection services)
    {
        services.AddSingleton<IHttpResponseFactory, HttpResponseFactory>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        
        return services;
    }
}
