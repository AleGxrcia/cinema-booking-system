using CinemaBookingSystem.Modules.Movies.Extensions;

namespace CinemaBookingSystem.Api.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder UseModules(this IApplicationBuilder app)
    {
        app.UseMoviesModule();
        
        return app;
    }
}
