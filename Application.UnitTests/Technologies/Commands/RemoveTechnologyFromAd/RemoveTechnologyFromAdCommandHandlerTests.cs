using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Commands.RemoveTechnologyFromAd;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Commands.RemoveTechnologyFromAd;

public class RemoveTechnologyFromAdCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task RemovingNonExistantTechnologyFromAdThrowsNotFound()
    {
        var ad = new Ad
        {
            WebPage = new Uri("https://test.com/")
        };

        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);


        var request = new RemoveTechnologyFromAdCommand(AdId: ad.Id, TechnologyId: 1);
        var handler = new RemoveTechnologyFromAdCommandHandler(Context);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task RemovingTechnologyFromNonExistantAdThrowsNotFound()
    {
        var technology = new Technology
        {
            Name = "tech"
        };

        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new RemoveTechnologyFromAdCommand(AdId: 1, TechnologyId: technology.Id);
        var handler = new RemoveTechnologyFromAdCommandHandler(Context);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task RemovingTechnologyFromAdUpdatesDatabase()
    {
        var ad = new Ad
        {
            WebPage = new Uri("https://test.com/")
        };

        var technology = new Technology
        {
            Name = "tech",
        };

        Context.Ads.Add(ad);
        Context.Technologies.Add(technology);
        ad.Technologies.Add(technology);

        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new RemoveTechnologyFromAdCommand(AdId: ad.Id, TechnologyId: technology.Id);
        var handler = new RemoveTechnologyFromAdCommandHandler(Context);

        await handler.Handle(request, CancellationToken.None);

        Assert.Empty(ad.Technologies);
    }
}
