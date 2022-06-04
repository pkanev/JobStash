using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Technologies;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Technologies.Commands.DeleteTechnology;

public record DeleteTechnologyCommand(int Id) : IRequest;

public class DeleteTechnologyCommandRequestHandler : IRequestHandler<DeleteTechnologyCommand, Unit>
{
    private readonly IApplicationDbContext context;

    public DeleteTechnologyCommandRequestHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Technologies
            .Include(t => t.Ads)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Technology), request.Id);

        if (entity.Ads.Any())
            throw new InvalidDeleteOperationException($"The technology \"{entity.Name}\" has associated ads and cannot be deleted.");

        entity.AddDomainEvent(new TechnologyDeletedEvent(entity));

        context.Technologies.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
