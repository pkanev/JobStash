using JobStash.Application.Ads.Commands.DeleteAd;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Ads.Commands.DeleteAd;

public class DeleteAdCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task DeletingNonExistantAdThrowsNotFound()
    {
        var request = new DeleteAdCommand(3);

        var handler = new DeleteAdCommandHandler(Context);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingAdRemovesEntityFromDatabase()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };
        Context.Companies.Add(company);

        var ad = new Ad
        {
            Company = company,
            WebPage = new Uri("https://test.com")
        };
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new DeleteAdCommand(ad.Id);

        var handler = new DeleteAdCommandHandler(Context);
        await handler.Handle(request, CancellationToken.None);

        var result = Context.Ads.Find(ad.Id);
        Assert.Null(result);
    }
}
