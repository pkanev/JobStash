using FluentValidation;
using JobStash.Application.Common.Constants;

namespace JobStash.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(Constants.COMPANY_NAME_MAX_LENGTH)
        .NotEmpty();
    }
}
