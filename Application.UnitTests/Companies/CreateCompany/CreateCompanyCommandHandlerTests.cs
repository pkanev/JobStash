using JobStash.Application.Common.Interfaces;
using JobStash.Application.Companies.Commands.CreateCompany;
using JobStash.Application.UnitTests.Mocks;
using JobStash.Domain.Entities;
using Moq;

namespace JobStash.Application.UnitTests.Companies.CreateCompany;

public class CreateCompanyCommandHandlerTests
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

        var expectedId = new Random().Next(1, 100);
        var contextMock = new ApplicationDbContextMock();
        contextMock.CompaniesMock
            .Setup(m => m.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()).Result)
            .Callback((Company c, CancellationToken ct) =>
            {
                c.Id = expectedId;
            });

        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateCompanyCommandHandler(contextMock, urlMock.Object);
        var actual = await handler.Handle(request, CancellationToken.None);
        Assert.Equal(expectedId, actual);
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

        var contextMock = new ApplicationDbContextMock();
        var urlMock = new Mock<IUrlHelper>();
        urlMock.Setup(m => m.GetUri(It.IsAny<string>())).Returns((string s) => new Uri(s));

        var handler = new CreateCompanyCommandHandler(contextMock, urlMock.Object);
        await Assert.ThrowsAnyAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
    }
}
