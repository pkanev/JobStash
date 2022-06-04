using JobStash.Domain.Entities;

namespace JobStash.Domain.UnitTests;

public class CompanyTests
{
    [Fact]
    public void NewCompanyHasEmptyAdsCollection()
    {
        Company company = new Company();
        Assert.Empty(company.Ads);
    }
}
