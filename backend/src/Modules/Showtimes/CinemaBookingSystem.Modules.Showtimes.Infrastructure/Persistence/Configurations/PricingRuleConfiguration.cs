using CinemaBookingSystem.Modules.Showtimes.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaBookingSystem.Modules.Showtimes.Infrastructure.Persistence.Configurations;

public class PricingRuleConfiguration : IEntityTypeConfiguration<PricingRule>
{
    public void Configure(EntityTypeBuilder<PricingRule> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Price).HasColumnType("decimal(18,2)");
    }
}
