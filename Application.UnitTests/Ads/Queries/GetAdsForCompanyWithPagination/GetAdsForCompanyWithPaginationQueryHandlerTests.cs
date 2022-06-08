using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Ads.Queries.GetAdsForCompanyWithPagination;
using JobStash.Application.Common.Models;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Ads.Queries.GetAdsForCompanyWithPagination;

public class GetAdsForCompanyWithPaginationQueryHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task GetAdsForNonExistantCompanyShouldReturnEmptyPaginatedList()
    {
        var request = new GetAdsForCompanyWithPaginationQuery();
        var handler = new GetAdsForCompanyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetAdsForCompanyWithNoAdsShouldReturnEmptyPaginatedList()
    {
        var company = new Company { Name = "Sofia Zmeys" };
        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetAdsForCompanyWithPaginationQuery { CompanyId = company.Id };
        var handler = new GetAdsForCompanyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetAdsShouldReturnPaginatedList()
    {
        var company = new Company { Name = "Sofia Zmeys" };
        Context.Companies.Add(company);
        var ads = new List<Ad>
        {
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/1")},
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/2")},
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/3")},
        };
        Context.Ads.AddRange(ads);

        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetAdsForCompanyWithPaginationQuery { CompanyId = company.Id };
        var handler = new GetAdsForCompanyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<PaginatedList<AdBriefDto>>(result);
        Assert.Equal(ads.Count, result.TotalCount);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(1, result.PageNumber);
        for (int i = 0; i < result.Items.Count; i++)
            Assert.Equal(ads[i].WebPage.ToString(), result.Items[i].WebPage);
    }
}
