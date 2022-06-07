using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Technologies;
using MediatR;

namespace JobStash.Application.Technologies.Commands.CreateTechnology;

public record CreateTechnologyCommand(string Name) : IRequest<int>;

public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, int>
{
    private readonly IApplicationDbContext context;

    public CreateTechnologyCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<int> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        if (context.Technologies.Any(t => t.Name.ToLower() == request.Name.ToLower()))
            throw new DuplicatingTechnologyExcpetion(request.Name);

        var entity = new Technology();
        entity.Name = request.Name;

        entity.AddDomainEvent(new TechnologyCreatedEvent(entity));

        await context.Technologies.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
