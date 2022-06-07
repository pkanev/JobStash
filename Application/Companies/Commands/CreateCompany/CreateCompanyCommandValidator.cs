using FluentValidation;
using JobStash.Application.Common.Constants;

namespace JobStash.Application.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(Constants.COMPANY_NAME_MAX_LENGTH)
        .NotEmpty();
    }
}
