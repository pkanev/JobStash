using JobStash.Application.Common.Exceptions;
using JobStash.Application.Companies.Commands.DeleteCompany;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task DeletingNonExistantCompanyThrowsNotFound()
    {
        var request = new DeleteCompanyCommand(3);

        var handler = new DeleteCompanyCommandHandler(Context);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingCompanyWithAdsThrowsInvalidDeleteOperation()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };

        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);

        var ad = new Ad
        {
            Company = company,
            WebPage = new Uri("http://test.com"),
        };
        Context.Ads.Add(ad);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new DeleteCompanyCommand(company.Id);

        var handler = new DeleteCompanyCommandHandler(Context);
        await Assert.ThrowsAsync<InvalidDeleteOperationException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingCompanyRemovesEntityFromDatabase()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };

        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);

        var request = new DeleteCompanyCommand(company.Id);

        var handler = new DeleteCompanyCommandHandler(Context);
        await handler.Handle(request, CancellationToken.None);

        var result = Context.Companies.Find(company.Id);
        Assert.Null(result);
    }
}