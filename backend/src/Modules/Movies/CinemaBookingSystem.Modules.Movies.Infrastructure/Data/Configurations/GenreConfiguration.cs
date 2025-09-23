using CinemaBookingSystem.Modules.Movies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Movies.Infrastructure.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property(g => g.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(g => g.IsActive)
            .IsRequired();

        builder.Property(g => g.CreatedAt)
            .IsRequired();

        builder.Property(g => g.UpdatedAt)
            .IsRequired(false);

        builder.HasIndex(g => g.Name)
            .IsUnique()
            .HasDatabaseName("IX_genres_name_unique");

        builder.HasIndex(g => g.IsActive)
            .HasDatabaseName("IX_genres_is_active");
    }
}