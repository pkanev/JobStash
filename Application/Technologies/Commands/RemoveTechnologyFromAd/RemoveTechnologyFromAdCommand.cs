using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;
using JobStash.Domain.Entities;
using MediatR;

namespace JobStash.Application.Technologies.Commands.RemoveTechnologyFromAd;

public record RemoveTechnologyFromAdCommand(int AdId, int TechnologyId) : IRequest;

public class RemoveTechnologyFromAdCommandRequestHandler : IRequestHandler<RemoveTechnologyFromAdCommand, Unit>
{
    private readonly IApplicationDbContext context;

    public RemoveTechnologyFromAdCommandRequestHandler(IApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(RemoveTechnologyFromAdCommand request, CancellationToken cancellationToken)
    {
        var ad = await context.Ads.FindAsync(new object[] { request.AdId }, cancellationToken);
        if (ad == null)
            throw new NotFoundException(nameof(Ad), request.AdId);

        var technology = await context.Technologies.FindAsync(new object[] { request.TechnologyId }, cancellationToken);
        if (technology == null)
            throw new NotFoundException(nameof(Technology), request.TechnologyId);

        ad.Technologies.Add(technology);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
