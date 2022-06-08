using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Companies;
using MediatR;

namespace JobStash.Application.Companies.Commands.CreateCompany;

public record CreateCompanyCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
    public string? WebPage { get; init; }
    public string? CareersPage { get; init; }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly IApplicationDbContext context;
    private readonly IUrlHelper urlHelper;

    public CreateCompanyCommandHandler(IApplicationDbContext context, IUrlHelper urlHelper)
    {
        this.context = context;
        this.urlHelper = urlHelper;
    }

    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Company();

        entity.Name = request.Name.Trim();

        if (!string.IsNullOrWhiteSpace(request.WebPage))
            entity.WebPage = urlHelper.GetUri(request.WebPage);

        if (!string.IsNullOrWhiteSpace(request.CareersPage))    
            entity.CareersPage = urlHelper.GetUri(request.CareersPage);

        entity.AddDomainEvent(new CompanyCreatedEvent(entity));

        await context.Companies.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
