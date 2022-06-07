using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace JobStash.Application.UnitTests.Mocks;

internal class ApplicationDbContextMock : IApplicationDbContext
{
    private readonly Mock<DbSet<Ad>> ads = new Mock<DbSet<Ad>>();
    private readonly Mock<DbSet<Company>> companies = new Mock<DbSet<Company>>();
    private readonly Mock<DbSet<Technology>> technologies = new Mock<DbSet<Technology>>();

    public Mock<DbSet<Ad>> AdsMock => ads;
    public Mock<DbSet<Company>> CompaniesMock => companies;
    public Mock<DbSet<Technology>> TechnologiesMock => technologies;

    public DbSet<Ad> Ads => ads.Object;

    public DbSet<Company> Companies => companies.Object;

    public DbSet<Technology> Technologies => technologies.Object;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => Task.FromResult(1);
}
