using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Companies.Commands.UpdateCompany;
using JobStash.Application.UnitTests.Context;
using JobStash.Domain.Entities;
using Moq;

namespace JobStash.Application.UnitTests.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandHandlerTests : ContextBaseTests
{
    [Fact]
    public async Task UpdatingNonExistingCompanyThrowsNotFound()
    {
        var request = new UpdateCompanyCommand
        {
            Id = 1,
            Name = "Sofia Zmeys"
        };

        var handler = new UpdateCompanyCommandHandler(Context, new Mock<IUrlHelper>().Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task UpdatingCompanyUpdatesEntityInDatabase()
    {
        var company = new Company
        {
            Name = "Old Name"
        };
        
        Context.Companies.Add(company);
        await Context.SaveChangesAsync(CancellationToken.None);
        string expectedName = "new name";
        var request = new UpdateCompanyCommand
        {
            Id = company.Id,
            Name = expectedName
        };

        var handler = new UpdateCompanyCommandHandler(Context, new Mock<IUrlHelper>().Object);
        await handler.Handle(request, CancellationToken.None);

        Assert.Equal(expectedName, company.Name);
    }
}
