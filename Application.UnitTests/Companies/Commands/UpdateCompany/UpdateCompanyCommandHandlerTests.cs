using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Companies.Commands.UpdateCompany;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;
using Moq;

namespace JobStash.Application.UnitTests.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandHandlerTests
{
    private readonly TestDbContext context;

    public UpdateCompanyCommandHandlerTests()
    {
        context = Tests.GetContext();
    }

    [Fact]
    public async Task UpdatingNonExistingCompanyThrowsNotFound()
    {
        var request = new UpdateCompanyCommand
        {
            Id = 1,
            Name = "Sofia Zmeys"
        };

        var handler = new UpdateCompanyCommandHandler(context, new Mock<IUrlHelper>().Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task UpdatingCompanyUpdatesEntityInDatabase()
    {
        var company = new Company
        {
            Name = "Old Name"
        };
        
        context.Companies.Add(company);
        await context.SaveChangesAsync();
        string expectedName = "new name";
        var request = new UpdateCompanyCommand
        {
            Id = company.Id,
            Name = expectedName
        };

        var handler = new UpdateCompanyCommandHandler(context, new Mock<IUrlHelper>().Object);
        await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expectedName, company.Name);
    }
}
