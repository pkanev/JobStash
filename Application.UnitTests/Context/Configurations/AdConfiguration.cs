using JobStash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobStash.Application.UnitTests.Context.Configurations;

internal class AdConfiguration : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        builder.Property(a => a.WebPage)
                .HasConversion(v => v.ToString(), v => new Uri(v));

        builder.HasMany(a => a.Technologies)
        .WithMany(t => t.Ads);
    }
}
