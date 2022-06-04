using FluentValidation;

namespace JobStash.Application.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(200)
        .NotEmpty();
    }
}
