using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Models;
using JobStash.Application.Technologies.Queries.Dtos;
using JobStash.Application.Technologies.Queries.GetTechnologiesForAdWithPagination;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Queries.GetTechnologiesForAdWithPagination;

public class GetTechnologiesForAdWithPaginationQueryHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task GetTechnologiesWhenAdNonExistentThrowsNotFound()
    {
        var request = new GetTechnologiesForAdWithPaginationQuery()
        {
            AdId = 1,
        };

        var handler = new GetTechnologiesForAdWithPaginationQueryHandler(Context, Mapper);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task GetTechnologiesWhenNoTechnologiesShouldReturnEmptyPaginatedList()
    {
        var ad = new Ad { WebPage = new Uri("http://test.com") };
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetTechnologiesForAdWithPaginationQuery()
        {
            AdId = ad.Id,
        };

        var handler = new GetTechnologiesForAdWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }

    [Fact]
    public async Task GetTechnologiesForAdShouldReturnPaginatedListOrderedByName()
    {
        var technologies = new List<Technology>
        {
            new Technology { Name = "T3" },
            new Technology { Name = "T2" },
            new Technology { Name = "T1" }
        };

        Context.Technologies.AddRange(technologies);

        var ad = new Ad { WebPage = new Uri("http://test.com") };
        Context.Ads.Add(ad);

        ad.Technologies.Add(technologies[0]);
        ad.Technologies.Add(technologies[2]);

        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new GetTechnologiesForAdWithPaginationQuery { AdId = ad.Id };
        var handler = new GetTechnologiesForAdWithPaginationQueryHandler(Context, Mapper);

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<PaginatedList<TechnologyDto>>(result);
        Assert.Equal(ad.Technologies.Count, result.TotalCount);
        Assert.Equal(1, result.TotalPages);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal("T1", result.Items.First().Name);
        Assert.Equal("T3", result.Items.Last().Name);
    }
}
