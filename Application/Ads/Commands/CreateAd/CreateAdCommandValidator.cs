using FluentValidation;

namespace JobStash.Application.Ads.Commands.CreateAd;

public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
{
    public CreateAdCommandValidator()
    {
        RuleFor(v => v.CompanyId)
            .GreaterThan(0);

        RuleFor(v => v.WebPage)
        .NotEmpty();
    }
}
