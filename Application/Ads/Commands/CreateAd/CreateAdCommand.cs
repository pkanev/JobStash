using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Ads;
using MediatR;

namespace JobStash.Application.Ads.Commands.CreateAd;

public record CreateAdCommand : IRequest<int>
{
    public int CompanyId { get; init; }
    public string WebPage { get; init; } = null!;
    public int[]? TechnologyIds { get; init; }
}

public class CreateAdCommandRequestHandler : IRequestHandler<CreateAdCommand, int>
{
    private readonly IApplicationDbContext context;
    private readonly IUrlHelper urlHelper;

    public CreateAdCommandRequestHandler(IApplicationDbContext context, IUrlHelper urlHelper)
    {
        this.context = context;
        this.urlHelper = urlHelper;
    }
    public async Task<int> Handle(CreateAdCommand request, CancellationToken cancellationToken)
    {
        var entity = new Ad();

        var company = await context.Companies
            .FindAsync(new object[] { request.CompanyId }, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Company), request.CompanyId);

        if (request.TechnologyIds != null)
            foreach (var technology in GetTechnologies(request.TechnologyIds))
                entity.Technologies.Add(technology);

        entity.Published = false;
        entity.Expired = false;
        entity.WebPage = urlHelper.GetUri(request.WebPage);

        entity.AddDomainEvent(new AdCreatedEvent(entity));

        await context.Ads.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    private List<Technology> GetTechnologies( int[] technologyIds) =>
        context.Technologies.Where(t => technologyIds.Contains(t.Id)).ToList();
}
