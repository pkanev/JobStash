using JobStash.Application.Common.Constants;
using JobStash.Application.Companies.Commands.CreateCompany;

namespace JobStash.Application.UnitTests.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidatorTests
{
    [Fact]
    public void NewComapnyShouldNotHaveNullName()
    {
        var validator = new CreateCompanyCommandValidator();
        var request = new CreateCompanyCommand();
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateCompanyCommand.Name)));
    }

    [Fact]
    public void CompanyNameCannotExceedMaxLength()
    {
        var validator = new CreateCompanyCommandValidator();
        var request = new CreateCompanyCommand
        {
            Name = new string('A', Constants.COMPANY_NAME_MAX_LENGTH + 1)
        };
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.NotNull(result.Errors.First(o => o.PropertyName == nameof(CreateCompanyCommand.Name)));
    }
}
