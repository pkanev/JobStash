using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Technologies.Commands.UpdateTechnology;

public record UpdateTechnologyCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}

public class UpdateTechnologyCommandRequestHandler : IRequestHandler<UpdateTechnologyCommand, Unit>
{
    private readonly IApplicationDbContext context;

    public UpdateTechnologyCommandRequestHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Technologies.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Technology), request.Id);

        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
