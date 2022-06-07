using JobStash.Application.Common.Exceptions;
using JobStash.Application.Companies.Commands.DeleteCompany;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;

namespace JobStash.Application.UnitTests.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandHandlerTests
{
    private readonly TestDbContext context;

    public DeleteCompanyCommandHandlerTests()
    {
        context = Tests.GetContext();
    }

    [Fact]
    public async Task DeletingNonExistantCompanyThrowsNotFound()
    {
        var request = new DeleteCompanyCommand(3);

        var handler = new DeleteCompanyCommandHandler(context);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingCompanyWithAdsThrowsInvalidDeleteOperation()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };

        context.Companies.Add(company);
        await context.SaveChangesAsync();

        var ad = new Ad
        {
            Company = company,
            WebPage = new Uri("http://test.com"),
        };
        context.Ads.Add(ad);
        await context.SaveChangesAsync();

        var request = new DeleteCompanyCommand(company.Id);

        var handler = new DeleteCompanyCommandHandler(context);
        await Assert.ThrowsAsync<InvalidDeleteOperationException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task DeletingCompanyRemovesEntityFromDatabase()
    {
        var company = new Company
        {
            Name = "Sofia Zmeys"
        };

        context.Companies.Add(company);
        await context.SaveChangesAsync();

        var request = new DeleteCompanyCommand(company.Id);

        var handler = new DeleteCompanyCommandHandler(context);
        await handler.Handle(request, CancellationToken.None);

        var result = context.Companies.Find(company.Id);
        Assert.Null(result);
    }
}