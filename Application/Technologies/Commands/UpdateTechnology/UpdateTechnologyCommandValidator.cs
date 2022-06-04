using FluentValidation;

namespace JobStash.Application.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty();
    }
}
