using JobStash.Application.Common.Constants;
using JobStash.Application.Companies.Commands.UpdateCompany;

namespace JobStash.Application.UnitTests.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CompanyShouldNotHaveNullOrEmptyName(string name)
    {
        var validator = new UpdateCompanyCommandValidator();
        var request = new UpdateCompanyCommand
        {
            Id = 1,
            Name = name
        };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateCompanyCommand.Name)));
    }

    [Fact]
    public void CompanyNameCannotExceedMaxLength()
    {
        var validator = new UpdateCompanyCommandValidator();
        var request = new UpdateCompanyCommand
        {
            Id = 1,
            Name = new string('A', Constants.COMPANY_NAME_MAX_LENGTH + 1)
        };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(UpdateCompanyCommand.Name)));
    }
}
