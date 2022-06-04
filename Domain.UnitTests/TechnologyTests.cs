using JobStash.Domain.Entities;

namespace JobStash.Domain.UnitTests;

public class TechnologyTests
{
    [Fact]
    public void NewTechnologyHasEmptyAdsCollection()
    {
        Technology technology = new Technology();
        Assert.Empty(technology.Ads);
    }
}
