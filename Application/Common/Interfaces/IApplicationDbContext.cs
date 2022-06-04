using JobStash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Ad> Ads { get; }
    DbSet<Company> Companies { get; }
    DbSet<Technology> Technologies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
