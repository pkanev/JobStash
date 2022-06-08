using JobStash.Application.Common.Exceptions;
using JobStash.Application.Technologies.Commands.DeleteTechnology;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task DeletingNonExistantTechnologyThrowsNotFound()
    {
        var request = new DeleteTechnologyCommand(3);

        var handler = new DeleteTechnologyCommandHandler(Context);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingTechnologyWithAdsThrowsInvalidDeleteOperation()
    {
        var technology = new Technology
        {
            Name = "Tech"
        };

        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var ad = new Ad
        {
            WebPage = new Uri("http://test.com"),
        };
        ad.Technologies.Add(technology);
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new DeleteTechnologyCommand(technology.Id);

        var handler = new DeleteTechnologyCommandHandler(Context);
        await Assert.ThrowsAsync<InvalidDeleteOperationException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingCompanyRemovesEntityFromDatabase()
    {
        var technology = new Technology
        {
            Name = "Tech"
        };

        Context.Technologies.Add(technology);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new DeleteTechnologyCommand(technology.Id);

        var handler = new DeleteTechnologyCommandHandler(Context);
        await handler.Handle(request, CancellationToken.None);

        var result = Context.Companies.Find(technology.Id);
        Assert.Null(result);
    }
}
