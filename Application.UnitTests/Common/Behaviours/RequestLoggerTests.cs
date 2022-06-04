using JobStash.Application.Common.Behaviours;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Companies.Commands.CreateCompany;
using Microsoft.Extensions.Logging;
using Moq;

namespace JobPool.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateCompanyCommand>> logger = null!;
    private Mock<ICurrentUserService> currentUserService = null!;
    private Mock<IIdentityService> identityService = null!;

    public RequestLoggerTests()
    {
        logger = new Mock<ILogger<CreateCompanyCommand>>();
        currentUserService = new Mock<ICurrentUserService>();
        identityService = new Mock<IIdentityService>();
    }

    [Fact]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        currentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateCompanyCommand>(logger.Object, currentUserService.Object, identityService.Object);

        await requestLogger.Process(new CreateCompanyCommand { Name = "Sofia Zmeys", WebPage="https://www.sofiazmeys.freedom", CareersPage = "https://www.sofiazmeys.freedom/jobs" }, new CancellationToken());

        identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateCompanyCommand>(logger.Object, currentUserService.Object, identityService.Object);

        await requestLogger.Process(new CreateCompanyCommand { Name = "Sofia Zmeys" }, new CancellationToken());

        identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}
