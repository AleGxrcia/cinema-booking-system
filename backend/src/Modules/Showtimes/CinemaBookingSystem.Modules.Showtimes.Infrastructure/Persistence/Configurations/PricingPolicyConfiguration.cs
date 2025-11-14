using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence.Configurations;

public class PricingPolicyConfiguration : IEntityTypeConfiguration<PricingPolicy>
{
    public void Configure(EntityTypeBuilder<PricingPolicy> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(p => p.Rules)
               .WithOne()
               .HasForeignKey("PricingPolicyId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
