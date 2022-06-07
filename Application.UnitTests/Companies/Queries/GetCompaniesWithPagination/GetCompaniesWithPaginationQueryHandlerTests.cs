using JobStash.Application.Companies.Queries.GetCompaniesWithPagination;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Companies.Queries.GetCompaniesWithPagination;

public class GetCompaniesWithPaginationQueryHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task GetCompaniesWhenNoCompaniesShouldReturnEmptyPaginatedList()
    {
        var request = new GetCompaniesWithPaginationQuery();
        var handler = new GetCompaniesWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetCompaniesShouldReturnPaginatedListOrderedByName()
    {
        Context.Companies.AddRange(
            new Company { Name = "C3" },
            new Company { Name = "C2" },
            new Company { Name = "C1" }
        );

        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetCompaniesWithPaginationQuery();
        var handler = new GetCompaniesWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<List<CompanyBriefDto>>(result.Items);
        Assert.Equal(Context.Companies.Count(), result.TotalCount);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal("C1", result.Items.First().Name);
        Assert.Equal("C3", result.Items.Last().Name);
    }
}
