using JobStash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobStash.Application.UnitTests.Context.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            builder.Property(c=> c.WebPage)
                .HasConversion(v => v != null ? v.ToString() : null, v => new Uri(v) );
#pragma warning restore CS8604 // Possible null reference argument.

#pragma warning disable CS8604 // Possible null reference argument.
            builder.Property(c => c.CareersPage)
                .HasConversion(v => v != null ? v.ToString() : null, v => new Uri(v));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
