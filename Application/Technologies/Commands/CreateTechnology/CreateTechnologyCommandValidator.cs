using FluentValidation;

namespace JobStash.Application.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
    }
}
