using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Ads.Queries.GetAd;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Ads.Queries.GetAd;

public class GetAdQueryHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task GetNonExistentAdThrowsNotFound()
    {
        var random = new Random();
        var request = new GetAdQuery(random.Next(1, 100));

        var handler = new GetAdQueryHandler(Context, Mapper);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task GetCompanyReturnsAdDto()
    {
        var company = new Company { Name = "Sofia Zmeys" };
        Context.Companies.Add(company);
        var ad = new Ad { Company = company, WebPage = new Uri("http://ads.com") };
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var handler = new GetAdQueryHandler(Context, Mapper);
        var result = await handler.Handle(new GetAdQuery(ad.Id), CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<AdDto>(result);
        Assert.Equal(ad.Id, result.Id);
        Assert.Equal(ad.WebPage.ToString(), result.WebPage);
    }
}
