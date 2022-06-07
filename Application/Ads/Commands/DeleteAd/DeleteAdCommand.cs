using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using JobStash.Domain.Events.Ads;
using MediatR;

namespace JobStash.Application.Ads.Commands.DeleteAd;

public record DeleteAdCommand(int Id) : IRequest;

public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand>
{
    private readonly IApplicationDbContext context;

    public DeleteAdCommandHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Ads
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Ad), request.Id);

        context.Ads.Remove(entity);

        entity.AddDomainEvent(new AdDeletedEvent(entity));

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
