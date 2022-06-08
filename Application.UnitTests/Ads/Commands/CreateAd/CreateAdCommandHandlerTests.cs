using JobStash.Application.Ads.Commands.CreateAd;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;
using Moq;

namespace JobStash.Application.UnitTests.Ads.Commands.CreateAd;

public class CreateAdCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task AddingAnAdShouldReturnAdId()
    {
        var company = new Company { Name = "Sofia Zmeys", WebPage = new Uri("http://test.com") };
        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new CreateAdCommand
        {
            CompanyId = company.Id,
            WebPage = "https://test.com",
        };

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateAdCommandHandler(Context, urlMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);
        var ad = Context.Ads.Find(result);

        Assert.IsType<int>(result);
        Assert.NotNull(ad);
        Assert.Equal(ad?.Id, result);
    }

    [Fact]
    public async Task AddingAnAdToNonExistentCompanyShouldThrowNotFound()
    {
        var request = new CreateAdCommand
        {
            CompanyId = 1,
            WebPage = "https://test.com",
        };

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateAdCommandHandler(Context, urlMock.Object);
        await Assert.ThrowsAnyAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }
}
