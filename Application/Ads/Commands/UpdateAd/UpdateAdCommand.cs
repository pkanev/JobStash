using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Ads.Commands.UpdateAd;

public record UpdateAdCommand : IRequest
{
    public int Id { get; init; }
    public int CompanyId { get; init; }
    public string WebPage { get; init; } = null!;
    public bool Published { get; init; }
    public bool Expired { get; init; }
}

public class UpdateAdCommandRequestHandler : IRequestHandler<UpdateAdCommand>
{
    private readonly IApplicationDbContext context;
    private readonly IUrlHelper urlHelper;

    public UpdateAdCommandRequestHandler(IApplicationDbContext context, IUrlHelper urlHelper)
    {
        this.context = context;
        this.urlHelper = urlHelper;
    }

    public async Task<Unit> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Ads
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Ad), request.Id);

        var company = await context.Companies
            .FindAsync(new object[] { request.CompanyId }, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Company), request.CompanyId);

        entity.Company = company;
        entity.WebPage = urlHelper.GetUri(request.WebPage);
        entity.Published = request.Published;
        entity.Expired = request.Expired;

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
