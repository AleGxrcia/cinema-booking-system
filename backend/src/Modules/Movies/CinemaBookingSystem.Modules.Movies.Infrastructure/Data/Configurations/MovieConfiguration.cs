using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using CinemaBookingSystem.Modules.Movies.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever();

        builder.ComplexProperty(m => m.Title, titlebuilder =>
        {
            titlebuilder.Property(t => t.Value)
                .HasColumnName("title")
                .HasMaxLength(200)
                .IsRequired();

            titlebuilder.Property(t => t.NormalizedValue)
                .HasColumnName("normalized_title")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.Property(m => m.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.ComplexProperty(m => m.Duration, durationBuilder =>
        {
            durationBuilder.Property(d => d.TotalMinutes)
                .HasColumnName("duration_minutes")
                .IsRequired();
        });

        builder.Property(m => m.ReleaseYear)
            .IsRequired();

        builder.Property(m => m.PosterUrl)
            .HasMaxLength(500);

        builder.Property(m => m.TrailerUrl)
            .HasMaxLength(500);

        builder.Property(m => m.AgeRating)
            .IsRequired();

        builder.Property(m => m.Status)
            .IsRequired();

        builder.ComplexProperty(m => m.Language, languageBuilder =>
        {
            languageBuilder.Property(l => l.Code)
                .HasColumnName("language_code")
                .HasMaxLength(3)
                .IsRequired();

            languageBuilder.Property(l => l.Name)
                .HasColumnName("language_name")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.Property(m => m.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        builder.Property(m => m.UpdatedAt)
            .IsRequired(false);

        builder.HasMany(m => m.Genres)
            .WithOne(mg => mg.Movie)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Cast)
            .WithOne(mc => mc.Movie)
            .HasForeignKey(mc => mc.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.Status)
            .HasDatabaseName("IX_movies_status");

        builder.HasIndex(m => m.ReleaseYear)
            .HasDatabaseName("IX_movies_release_year");

        builder.HasIndex(m => m.CreatedAt)
            .HasDatabaseName("IX_movies_created_at");
    }
}