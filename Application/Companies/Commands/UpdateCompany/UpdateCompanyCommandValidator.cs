using FluentValidation;

namespace JobStash.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(200)
        .NotEmpty();
    }
}
