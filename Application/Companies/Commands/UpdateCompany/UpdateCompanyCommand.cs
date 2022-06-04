using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string? WebPage { get; init; }
    public string? CareersPage { get; init; }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
{
    private readonly IApplicationDbContext context;
    private readonly IUrlHelper urlHelper;

    public UpdateCompanyCommandHandler(IApplicationDbContext context, IUrlHelper urlHelper)
    {
        this.context = context;
        this.urlHelper = urlHelper;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Companies
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Company), request.Id);

        entity.Name = request.Name;
        entity.WebPage = !string.IsNullOrEmpty(request.WebPage) 
            ? urlHelper.GetUri(request.WebPage)
            : null;

        entity.CareersPage = !string.IsNullOrWhiteSpace(request.CareersPage)
            ? urlHelper.GetUri(request.CareersPage)
            : null;

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
