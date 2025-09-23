using CinemaBookingSystem.Shared.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Shared.Presentation.Extensions;

public static class MigrationExtensions
{
    public static IApplicationBuilder ApplyMigrations<TDbContext>(this IApplicationBuilder app)
        where TDbContext : DbContext
    {
        var task = MigrationService.ApplyMigrationsAsync<TDbContext>(app.ApplicationServices);
        task.GetAwaiter().GetResult();
        return app;
    }
}
