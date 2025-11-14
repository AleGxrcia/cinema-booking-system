using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence;

public class ShowtimesDbContext : DbContext
{
    public DbSet<Hall> Halls { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }
    public DbSet<PricingPolicy> PricingPolicies { get; set; }
    public DbSet<PricingRule> PricingRules { get; set; }

    public ShowtimesDbContext(DbContextOptions<ShowtimesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowtimesDbContext).Assembly);
    }
}
