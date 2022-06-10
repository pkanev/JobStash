using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JobStash.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator mediator;
    private readonly AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        this.mediator = mediator;
        this.auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Ad> Ads => Set<Ad>();

    public DbSet<Company> Companies => Set<Company>();

    public DbSet<Technology> Technologies => Set<Technology>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
