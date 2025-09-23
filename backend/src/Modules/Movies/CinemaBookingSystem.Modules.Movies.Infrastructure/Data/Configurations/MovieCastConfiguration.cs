using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Configurations;

public class MovieCastConfiguration : IEntityTypeConfiguration<MovieCast>
{
    public void Configure(EntityTypeBuilder<MovieCast> builder)
    {
        builder.HasKey(mc => mc.Id);

        builder.Property(mc => mc.Id)
            .ValueGeneratedNever();

        builder.Property(mc => mc.MovieId)
            .IsRequired();

        builder.Property(mc => mc.PersonName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(mc => mc.Order)
            .IsRequired();

        builder.Property(mc => mc.CreatedAt)
            .IsRequired();

        builder.Property(mc => mc.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(mc => mc.Movie)
            .WithMany(m => m.Cast)
            .HasForeignKey(mc => mc.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(mc => mc.MovieId)
            .HasDatabaseName("IX_movie_casts_movie_id");

        builder.HasIndex(mc => mc.Role)
            .HasDatabaseName("IX_movie_casts_role");

        builder.HasIndex(mc => new { mc.MovieId, mc.Order })
            .HasDatabaseName("IX_movie_casts_movie_order");

        builder.HasIndex(mc => mc.PersonName)
            .HasDatabaseName("IX_movie_casts_person_name");
    }
}