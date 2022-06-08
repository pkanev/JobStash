using JobStash.Application.Ads.Commands.CreateAd;

namespace JobStash.Application.UnitTests.Ads.Commands.CreateAd;

public class CreateAdCommandValidatorTests
{
    [Fact]
    public void NewAdShouldNotHaveNullWebPage()
    {
        var validator = new CreateAdCommandValidator();
        var request = new CreateAdCommand { CompanyId = 1};
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateAdCommand.WebPage)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NewAdShouldNotHaveZeroOrNegativeCompanyId(int id)
    {
        var validator = new CreateAdCommandValidator();
        var request = new CreateAdCommand { CompanyId = id, WebPage = "http://test.com"};
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateAdCommand.CompanyId)));
    }
}
