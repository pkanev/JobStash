using JobStash.Application.Ads.Commands.UpdateAd;

namespace JobStash.Application.UnitTests.Ads.Commands.UpdateAd;

public class UpdateAdCommandValidatorTests
{
    [Fact]
    public void UpdatedAdShouldNotHaveNullWebPage()
    {
        var validator = new UpdateAdCommandValidator();
        var request = new UpdateAdCommand { CompanyId = 1, Id = 1 };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateAdCommand.WebPage)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdatedAdShouldNotHaveZeroOrNegativeCompanyId(int id)
    {
        var validator = new UpdateAdCommandValidator();
        var request = new UpdateAdCommand { CompanyId = id, Id = 1, WebPage = "http://test.com" };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateAdCommand.CompanyId)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void UpdatedAdShouldNotHaveZeroOrNegativeId(int id)
    {
        var validator = new UpdateAdCommandValidator();
        var request = new UpdateAdCommand { CompanyId = 1, Id = id, WebPage = "http://test.com" };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateAdCommand.Id)));
    }
}
