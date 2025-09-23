using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBookingSystem.Shared.Infrastructure.Data.Services;

public static class MigrationService
{
    public static async Task ApplyMigrationsAsync<TDbContext>(IServiceProvider service,
        CancellationToken cancellationToken = default)
        where TDbContext : DbContext
    {
        await using var scope = service.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}
