using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Ads.Queries.GetAdsForTechnologyWithPagination;
using JobStash.Application.Common.Models;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Ads.Queries.GetAdsForTechnologyWithPagination;

public class GetAdsForTechnologyWithPaginationQueryTests : ContextBaseTests
{
    [Fact]
    public async Task GetAdsForNonExistantTechnologyShouldReturnEmptyPaginatedList()
    {
        var request = new GetAdsForTechnologyWithPaginationQuery();
        var handler = new GetAdsForTechnologyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetAdsForTechnologyWithNoAdsShouldReturnEmptyPaginatedList()
    {
        var technology = new Technology { Name = "tech" };
        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetAdsForTechnologyWithPaginationQuery { TechnologyId = technology.Id };
        var handler = new GetAdsForTechnologyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetAdsShouldReturnPaginatedList()
    {
        var technology = new Technology { Name = "tech" };
        Context.Technologies.Add(technology);
        var company = new Company { Name = "Sofia Zmeys" };
        Context.Companies.Add(company);
        var ads = new List<Ad>
        {
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/1")},
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/2")},
            new Ad{ Company = company, WebPage = new Uri("http://ads.com/3")},
        };

        ads[0].Technologies.Add(technology);
        ads[2].Technologies.Add(technology);
        Context.Ads.AddRange(ads);

        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetAdsForTechnologyWithPaginationQuery { TechnologyId = technology.Id };
        var handler = new GetAdsForTechnologyWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<PaginatedList<AdBriefDto>>(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(ads[0].WebPage.ToString(), result.Items[0].WebPage);
        Assert.Equal(ads[2].WebPage.ToString(), result.Items[1].WebPage);
    }
}
