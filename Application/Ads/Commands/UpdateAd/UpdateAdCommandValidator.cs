using FluentValidation;

namespace JobStash.Application.Ads.Commands.UpdateAd;

public class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
{
    public UpdateAdCommandValidator()
    {
        RuleFor(v => v.CompanyId)
            .GreaterThan(0);

        RuleFor(v => v.WebPage)
        .NotEmpty();
    }
}
