using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Commands.AddTechnologyToAd;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Commands.AddTechnologyToAd;

public class AddTechnologyToAdCommandHandlerTests :ContextBaseTests
{
    [Fact]
    public async Task AddingNonExistantTechnologyToAdThrowsNotFound()
    {
        var ad = new Ad
        {
            WebPage = new Uri("https://test.com/")
        };

        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);


        var request = new AddTechnologyToAdCommand(AdId: ad.Id, TechnologyId: 1);
        var handler = new AddTechnologyToAdCommandHandler(Context);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task AddingTechnologyToNonExistantAdThrowsNotFound()
    {
        var technology = new Technology
        {
            Name = "tech"
        };

        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new AddTechnologyToAdCommand(AdId: 1, TechnologyId: technology.Id);
        var handler = new AddTechnologyToAdCommandHandler(Context);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task AddingTechnologyToAdSavesInDatabase()
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
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new AddTechnologyToAdCommand(AdId: ad.Id, TechnologyId: technology.Id);
        var handler = new AddTechnologyToAdCommandHandler(Context);

        await handler.Handle(request, CancellationToken.None);

        Assert.NotEmpty(ad.Technologies);
        Assert.Equal(technology.Id, ad.Technologies.FirstOrDefault()?.Id);
    }
}
