using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobStash.Application.UnitTests.Context;

internal class TestDbContext : DbContext, IApplicationDbContext
{
    public TestDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Ad> Ads => Set<Ad>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Technology> Technologies => Set<Technology>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
