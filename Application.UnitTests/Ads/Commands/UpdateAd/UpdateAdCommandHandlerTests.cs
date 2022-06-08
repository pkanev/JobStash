using JobStash.Application.Ads.Commands.UpdateAd;
using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;
using Moq;

namespace JobStash.Application.UnitTests.Ads.Commands.UpdateAd;

public class UpdateAdCommandHandlerTests : ContextBaseTests
{
    //UpdateAdCommandHandler
    //UpdateAdCommand
    [Fact]
    public async Task UpdatingNonExistingAdThrowsNotFound()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };
        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new UpdateAdCommand
        {
            Id = 1,
            CompanyId = company.Id,
            WebPage = "https://test.com"
        };

        var handler = new UpdateAdCommandHandler(Context, new Mock<IUrlHelper>().Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task UpdatingAdWithNonExistingCompanyThrowsNotFound()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };
        var ad = new Ad
        {
            Company = company,
            WebPage = new Uri("https://ad.com/")
        };

        Context.Companies.Add(company);
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new UpdateAdCommand
        {
            Id = ad.Id,
            CompanyId = company.Id + 1,
            WebPage = "https://test.com"
        };

        var handler = new UpdateAdCommandHandler(Context, new Mock<IUrlHelper>().Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task UpdatingCompanyUpdatesEntityInDatabase()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };
        var ad = new Ad
        {
            Company = company,
            WebPage = new Uri("https://ad.com/")
        };

        Context.Companies.Add(company);
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        string expectedUrl = "https://test.com/";
        var request = new UpdateAdCommand
        {
            Id = ad.Id,
            CompanyId = company.Id,
            WebPage = expectedUrl
        };

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));
        var handler = new UpdateAdCommandHandler(Context, urlMock.Object);
        await handler.Handle(request, CancellationToken.None);
        var updatedAd = Context.Ads.Find(ad.Id);

        Assert.Equal(expectedUrl, updatedAd?.WebPage.ToString());
    }
}
