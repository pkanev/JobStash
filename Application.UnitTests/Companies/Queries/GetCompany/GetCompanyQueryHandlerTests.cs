using JobStash.Application.Common.Exceptions;
using JobStash.Application.Companies.Queries.GetCompany;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Companies.Queries.GetCompany;

public class GetCompanyQueryHandlerTests
{
    private readonly TestDbContext context;

    public GetCompanyQueryHandlerTests()
    {
        context = Tests.GetContext();
    }

    [Fact]
    public async Task GetgNonExistentCompanyThrowsNotFound()
    {
        var random = new Random();
        var request = new GetCompanyQuery(random.Next(1, 100));

        var handler = new GetCompanyQueryHandler(context, Tests.Mapper);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task GetCompanyReturnsCompanyDto()
    {
        var company = new Company { Name = "Sofia Zmeys" };
        context.Companies.Add(company);
        await context.SaveChangesAsync();

        var handler = new GetCompanyQueryHandler(context, Tests.Mapper);
        var result = await handler.Handle(new GetCompanyQuery(company.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<CompanyDto>(result);
        Assert.Equal(company.Id, result.Id);
        Assert.Equal(company.Name, result.Name);
    }
}
