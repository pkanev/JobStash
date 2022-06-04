using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Utils;

namespace JobStash.Application.UnitTests.Common.Utils;

public class UrlHelperTests
{
    [Fact]
    public void GivenStringIsValidShouldReturnValidUri()
    {
        var url = "https://www.sofiazmeys.freedom/";
        var helper = new UrlHelper();

        var actual = helper.GetUri(url);

        Assert.NotNull(actual);
        Assert.Equal(url, actual.ToString());
    }

    [Fact]
    public void GivenStringIsInvalidShouldThrow()
    {
        var helper = new UrlHelper();

        Assert.Throws<InvalidUrlException>(() => helper.GetUri("freedom"));
    }
}
