using JobStash.Application.Common.Interfaces;
using JobStash.Application.Companies.Commands.CreateCompany;
using JobStash.Application.UnitTests.Context;
using Moq;

namespace JobStash.Application.UnitTests.Companies.Commands.CreateCompany;

public class CreateCompanyCommandHandlerTests : ContextBaseTests
{
    [Theory]
    [InlineData("Sofia Zmeys", null, null)]
    [InlineData("Sofia Zmeys", "http://www.test.com", null)]
    [InlineData("Sofia Zmeys", "http://www.test.com", "http://www.test.com/jobs")]
    public async Task AddingACompanyShouldReturnCompanyId(string name, string? webPage, string? careersPage)
    {
        var request = new CreateCompanyCommand
        {
            Name = name,
            WebPage = webPage,
            CareersPage = careersPage,
        };

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateCompanyCommandHandler(Context, urlMock.Object);
        var result = await handler.Handle(request, CancellationToken.None);
        var company = Context.Companies.Find(result);

        Assert.IsType<int>(result);
        Assert.NotNull(company);
        Assert.Equal(company?.Id, result);
    }

    [Theory]
    [InlineData("Sofia Zmeys", "test.com", null)]
    [InlineData("Sofia Zmeys", "", "com/jobs")]
    [InlineData("Sofia Zmeys", "com", "com/jobs")]
    public async Task UsingInvalidDataWhenAddingACompanyThrows(string name, string? webPage, string? careersPage)
    {
        var request = new CreateCompanyCommand
        {
            Name = name,
            WebPage = webPage,
            CareersPage = careersPage,
        };

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateCompanyCommandHandler(Context, urlMock.Object);
        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
    }
}
