using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Companies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(int Id) : IRequest;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteCompanyCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Companies
            .Include(c => c.Ads)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Company), request.Id);

        if (entity.Ads.Any())
            throw new InvalidDeleteOperationException($"The company \"{entity.Name}\" has associated ads and cannot be deleted.");

        context.Companies.Remove(entity);

        entity.AddDomainEvent(new CompanyDeletedEvent(entity));

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
