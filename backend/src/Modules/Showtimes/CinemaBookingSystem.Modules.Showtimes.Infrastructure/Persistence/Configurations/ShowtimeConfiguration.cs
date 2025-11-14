using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence.Configurations;

public class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasOne<PricingPolicy>()
               .WithMany()
               .HasForeignKey(s => s.PricingPolicyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
