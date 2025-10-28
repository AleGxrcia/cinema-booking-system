using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Shared.Infrastructure.Data.Schemas;
using Microsoft.EntityFrameworkCore;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Contexts;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieCast> MovieCasts { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Movies);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);

        base.OnModelCreating(modelBuilder);

    }
}
