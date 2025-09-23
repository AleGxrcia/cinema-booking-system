using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Configurations;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(mg => mg.Id);

        builder.Property(mg => mg.Id)
            .ValueGeneratedNever();

        builder.Property(mg => mg.MovieId)
            .IsRequired();

        builder.Property(mg => mg.GenreId)
            .IsRequired();

        builder.Property(mg => mg.CreatedAt)
            .IsRequired();

        builder.Property(mg => mg.UpdatedAt)
            .IsRequired(false);

        builder.HasOne(mg => mg.Movie)
            .WithMany(m => m.Genres)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mg => mg.Genre)
            .WithMany()
            .HasForeignKey(mg => mg.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(mg => new { mg.MovieId, mg.GenreId })
            .IsUnique()
            .HasDatabaseName("IX_movie_genres_movie_genre_unique");

        builder.HasIndex(mg => mg.MovieId)
            .HasDatabaseName("IX_movie_genres_movie_id");

        builder.HasIndex(mg => mg.GenreId)
            .HasDatabaseName("IX_movie_genres_genre_id");
    }
}