using CinemaBookingSystem.Modules.Movies.Application.Abstractions.Data;
using CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Contexts;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Persistence;

public class UnitOfWork(MovieDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
