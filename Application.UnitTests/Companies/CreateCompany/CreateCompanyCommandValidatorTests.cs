using JobStash.Application.Companies.Commands.CreateCompany;

namespace JobStash.Application.UnitTests.Companies.CreateCompany;

public class CreateCompanyCommandValidatorTests
{
    [Fact]
    public void Test()
    {
        var validator = new CreateCompanyCommandValidator();
        var request = new CreateCompanyCommand();
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateCompanyCommand.Name)));
    }
}
